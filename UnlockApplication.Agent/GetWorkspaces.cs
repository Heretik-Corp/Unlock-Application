using kCura.Relativity.Client;
using kCura.Relativity.Client.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnlockApplication.Agent
{
    public class GetWorkspaces
    {
        private readonly IRSAPIClient _client;

        public GetWorkspaces(IRSAPIClient client)
        {
            _client = client;
        }

    }
}
