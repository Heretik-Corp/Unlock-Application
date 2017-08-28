using kCura.Agent;
using System;
using UnlockApplication.Agent.Repositories;

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
                var workspaceHelper = new UnlockApplicationService(new WorkspaceRepository(this.Helper), new ApplicationRepository(this.Helper));
                workspaceHelper.UnlockApplicationsInEnvironment();
            }
            catch (Exception e)
            {
                this.RaiseError(e.Message, e.StackTrace);
            }
        }
    }
}
