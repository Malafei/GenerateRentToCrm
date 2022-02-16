using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;

namespace TestGenerator
{
    class Program
    { 


        static void Main(string[] args)
        {
            string connectionString = $"Connection String";
            CrmServiceClient crmService = new CrmServiceClient(connectionString);

            List<Guid> guidsCarsClass = EntityHelper.GetEntityCarclass(crmService);
            List<Guid> guidsContacts = EntityHelper.GetEntityContacts(crmService);
            Dictionary<Guid, Guid> guidsCars = EntityHelper.GetEntityCar(crmService);


            //40000 - 32881 - 31000 - 28000 - 20000 - 15000 - 10000 - 7000 - 4000
            for (int i = 0; i < 4000; i++)
            {
                Guid guid = new Rent(guidsCarsClass, guidsCars, guidsContacts, crmService).PushRentToCrm(crmService);

                Console.WriteLine($"№ {i+1} = guid: {guid}");

            }
            Console.WriteLine("Gotovo");

        }
    }
}
