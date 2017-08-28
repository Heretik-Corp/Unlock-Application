using System.Collections.Generic;

namespace UnlockApplication.Agent.Repositories
{
    public interface IWorkspaceRepository
    {
        IEnumerable<int> GetWorkspaceIds();
    }
}