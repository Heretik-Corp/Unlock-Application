using kCura.Agent;
using kCura.Relativity.Client;
using kCura.Relativity.Client.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnlockApplication.Agent
{
    [kCura.Agent.CustomAttributes.Name("Unlock Application")]
    [System.Runtime.InteropServices.Guid("9cee5f11-0d48-4a84-bcc2-a426d7335a57")]
    public class Agent : AgentBase
    {
        public override string Name => "Unlock Application";

        public override void Execute()
        {
            try
            {
                var rsapiService = this.Helper.GetServicesManager().CreateProxy<IRSAPIClient>(Relativity.API.ExecutionIdentity.System);
                var workspaceHelper = new UnlockApplicationService(rsapiService, this.Helper);
                workspaceHelper.UnlockApplicationsInEnvironment();
            }
            catch (Exception e)
            {
                this.RaiseError(e.Message, e.StackTrace);
            }
        }
    }
}
