using kCura.Relativity.Client;
using kCura.Relativity.Client.DTOs;
using Relativity.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnlockApplication.Agent.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly IHelper _helper;

        public ApplicationRepository(IHelper helper)
        {
            _helper = helper;
        }
        public bool DoesWorkspaceHaveApplication(int workspaceId, Guid applicationGuid)
        {
            using (var client = _helper.GetServicesManager().CreateProxy<IRSAPIClient>(ExecutionIdentity.System))
            {
                client.APIOptions.WorkspaceID = workspaceId;
                try
                {
                    var readResult = client.Repositories.RelativityApplication.ReadSingle(applicationGuid);
                }
                catch (Exception e) when (e.Message.Contains("Cannot find a canonical field"))
                {
                    return false;
                }
                return true;
            }
        }
        public IEnumerable<UnlockApplication> GetApplicationsToUnlock(int workspaceId)
        {
            using (var client = _helper.GetServicesManager().CreateProxy<IRSAPIClient>(ExecutionIdentity.System))
            {
                client.APIOptions.WorkspaceID = workspaceId;
                var result = client.Repositories.RDO.Query(new Query<RDO>
                {
                    ArtifactTypeGuid = Guid.Parse(ObjectTypeGuids.UnlockApplication),
                    Fields = FieldValue.AllFields,
                    Condition = new BooleanCondition(RelativityApplicationFieldNames.Locked, BooleanConditionEnum.EqualTo, true)
                });
                if (!result.Success)
                {
                    throw new ApplicationException(result.Message);
                }
                foreach (var artifact in result.Results)
                {
                    yield return new UnlockApplication { RDO = artifact.Artifact };
                }
            }
        }

        public void UnlockApplication(int workspaceId, UnlockApplication application)
        {
            using (var client = _helper.GetServicesManager().CreateProxy<IRSAPIClient>(ExecutionIdentity.System))
            {
                client.APIOptions.WorkspaceID = workspaceId;
                var dbContext = _helper.GetDBContext(workspaceId);
                //can't use the RSAPI to update locked app, who knew
                dbContext.ExecuteNonQuerySQLStatement($"Update [RelativityApplication] set locked = 0 where ArtifactId = {application.ArtifactID}");
            }
        }
    }
}
