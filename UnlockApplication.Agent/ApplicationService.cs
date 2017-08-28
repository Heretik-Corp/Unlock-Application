using kCura.Relativity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnlockApplication.Agent
{
    public class ApplicationService
    {
        private readonly IRSAPIClient _client;

        public ApplicationService(IRSAPIClient client)
        {
            _client = client;
        }
        public void UnlockApplication(int workspaceId)
        {
            
        }
    }
}
