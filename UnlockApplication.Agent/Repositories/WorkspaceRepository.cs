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
    public class WorkspaceRepository : IWorkspaceRepository
    {
        private readonly IHelper _helper;

        public WorkspaceRepository(IHelper helper)
        {
            _helper = helper;
        }

        public IEnumerable<int> GetWorkspaceIds()
        {
            using (var client = _helper.GetServicesManager().CreateProxy<IRSAPIClient>(ExecutionIdentity.System))
            {
                var queryResult = client.Repositories.Workspace.Query(new Query<Workspace>
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
}
