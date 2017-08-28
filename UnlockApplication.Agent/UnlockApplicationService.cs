using System;
using UnlockApplication.Agent.Repositories;

namespace UnlockApplication.Agent
{
    public class UnlockApplicationService
    {
        private readonly IWorkspaceRepository _workspaceRepo;
        private readonly IApplicationRepository _appRepo;

        public UnlockApplicationService(IWorkspaceRepository workspaceRepo, IApplicationRepository appRepo)
        {
            _workspaceRepo = workspaceRepo;
            _appRepo = appRepo;
        }
        public void UnlockApplicationsInEnvironment()
        {
            var artifactGuid = Guid.Parse(Application.Guid);
            var workspaces = _workspaceRepo.GetWorkspaceIds();
            foreach (var workspaceId in workspaces)
            {
                var check = _appRepo.DoesWorkspaceHaveApplication(workspaceId, artifactGuid);
                if (check)
                {
                    this.UnlockApplicationsInWorkspace(workspaceId);
                }
            }
        }

        protected virtual void UnlockApplicationsInWorkspace(int workspaceId)
        {
            var applications = _appRepo.GetApplicationsToUnlock(workspaceId);
            foreach (var artifact in applications)
            {
                _appRepo.UnlockApplication(workspaceId, artifact);
            }
        }

    }
}
