using kCura.Relativity.Client;
using Relativity.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnlockApplication.Agent
{
    public static class HelperExtentions
    {
        public static IRSAPIClient GetClientForWorkspace(this IHelper helper, int workspaceId, ExecutionIdentity identity = ExecutionIdentity.System)
        {
            var client = helper.GetServicesManager().CreateProxy<IRSAPIClient>(ExecutionIdentity.System);
            client.APIOptions.WorkspaceID = workspaceId;
            return client;
        }
    }
}
