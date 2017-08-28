using kCura.Relativity.Client;
using kCura.Relativity.Client.DTOs;
using Relativity.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnlockApplication.Agent
{
    public class UnlockApplicationService
    {
        private readonly IRSAPIClient _client;
        private readonly IHelper _helper;

        public UnlockApplicationService(IRSAPIClient client, IHelper helper)
        {
            _client = client;
            _helper = helper;
        }
        public void UnlockApplicationsInEnvironment()
        {

            var artifactGuid = Guid.Parse(Application.Guid);
            var workspaces = GetWorkspaceIds();
            foreach (var workspaceId in workspaces)
            {
                _client.APIOptions.WorkspaceID = workspaceId;
                try
                {
                    var readResult = _client.Repositories.RelativityApplication.ReadSingle(artifactGuid);

                }
                catch (Exception e)
                {
                    if (!e.Message.Contains("Cannot find a canonical field"))
                    {
                        continue;
                    }
                }
                this.UnlockApplicationsInWorkspace(workspaceId);
            }
        }

        private void UnlockApplicationsInWorkspace(int workspaceId)
        {
            var result = _client.Repositories.RDO.Query(new Query<RDO>
            {
                ArtifactTypeGuid = Guid.Parse(ObjectTypeGuids.UnlockApplication),
                Fields = FieldValue.AllFields,
                //Condition = new BooleanCondition(RelativityApplicationFieldNames.Locked, BooleanConditionEnum.EqualTo, true)
            });
            if (!result.Success)
            {
                throw new ApplicationException(result.Message);
            }
            foreach (var artifact in result.Results)
            {
                var a = new UnlockApplication();
                a.RDO = artifact.Artifact;
                var app = _client.Repositories.RelativityApplication.ReadSingle(Guid.Parse(a.AppGuid));
                if (app.Locked.GetValueOrDefault(true))
                {
                    var dbContext = _helper.GetDBContext(workspaceId);
                    //can't use the RSAPI to update locked app, who knew
                    dbContext.ExecuteNonQuerySQLStatement($"Update [RelativityApplication] set locked = 0 where ArtifactId = {app.ArtifactID}");
                }
            }
        }

        private IEnumerable<int> GetWorkspaceIds()
        {
            var queryResult = _client.Repositories.Workspace.Query(new Query<Workspace>
            {
                Fields = FieldValue.NoFields
            });
            if (!queryResult.Success)
            {
                throw new ApplicationException(queryResult.Message);
            }
            return queryResult.Results.Select(x => x.Artifact.ArtifactID).ToList();
        }
    }
}
