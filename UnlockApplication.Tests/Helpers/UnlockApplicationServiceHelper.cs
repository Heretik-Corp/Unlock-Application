using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnlockApplication.Agent;
using UnlockApplication.Agent.Repositories;

namespace UnlockApplication.Tests.Helpers
{
    public class UnlockApplicationServiceHelper : UnlockApplicationService
    {
        public UnlockApplicationServiceHelper(IWorkspaceRepository workspaceRepo, IApplicationRepository appRepo) : base(workspaceRepo, appRepo)
        {
        }

        protected override void UnlockApplicationsInWorkspace(int workspaceId)
        {
        }
    }
}
