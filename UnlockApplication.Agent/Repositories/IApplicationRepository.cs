using System;
using System.Collections.Generic;
using kCura.Relativity.Client;

namespace UnlockApplication.Agent.Repositories
{
    public interface IApplicationRepository
    {
        bool DoesWorkspaceHaveApplication(int workspaceId, Guid applicationGuid);
        IEnumerable<UnlockApplication> GetApplicationsToUnlock(int workspaceId);
        void UnlockApplication(int workspaceId, UnlockApplication application);
    }
}