using kCura.Relativity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnlockApplication.Agent;

namespace UnlockApplication.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RSAPIClient(new Uri("https://demo.heretik.io/relativity.services"), new UsernamePasswordCredentials("relativity.admin@kcura.com", "Test1234!"));
            new UnlockApplicationService(client, null).UnlockApplicationsInEnvironment();
            Console.Read();
        }
    }
}
