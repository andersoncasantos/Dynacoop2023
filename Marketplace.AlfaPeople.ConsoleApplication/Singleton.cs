using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.AlfaPeople.ConsoleApplication
{
    public class Singleton
    {
        public static CrmServiceClient GetService()
        {
            string url = "legacy";
            string clientId = "8267d645-aa51-4110-88cb-028d0547706b";
            string clientSecret = "o.y8Q~qcq.qQSZ5X2wIbNAhdhCeKshNHUmYikdmU";
            CrmServiceClient serviceClient = new CrmServiceClient($"AuthType=ClientSecret;Url=https://{url}.crm2.dynamics.com/;AppId={clientId};ClientSecret={clientSecret};");
            if (!serviceClient.CurrentAccessToken.Equals(null))
                Console.WriteLine("Conexão realizada com sucesso");
            else
                Console.WriteLine("Erro na conexão");

            return serviceClient;
        }
    }
}
