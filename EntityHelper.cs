using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGenerator
{
    public static class EntityHelper
    {
        public static EntityCollection GetEntityCollection(IOrganizationService service, string entytiName, params string[] columSets)
        {
            QueryExpression Carclassquery = new QueryExpression
            {
                EntityName = entytiName,
                ColumnSet = new ColumnSet(columSets)
            };
            EntityCollection collection = service.RetrieveMultiple(Carclassquery);
            return service.RetrieveMultiple(Carclassquery);
        }

        public static Entity GetEntity(IOrganizationService service, EntityReference entityRef, params string[] columSets)
        {
            return service.Retrieve(entityRef.LogicalName, entityRef.Id, new ColumnSet(columSets));
        }


        public static List<Guid> GetEntityCarclass(IOrganizationService service)
        {
            var carClassIds = new List<Guid>();
            var collection = EntityHelper.GetEntityCollection(service, "cds_carclass", "cds_carclassid");
            foreach (var item in collection.Entities)
            {
                carClassIds.Add(item.Id);
            }
            Console.WriteLine("Get Data Carclass load");
            return carClassIds;
        }

        public static List<Guid> GetEntityContacts(IOrganizationService service)
        {
            var ContactsIds = new List<Guid>();
            EntityCollection collection = EntityHelper.GetEntityCollection(service, "contact", "contactid");
            foreach (var item in collection.Entities)
            {
                ContactsIds.Add(item.Id);
            }
            Console.WriteLine("Get Data Contacts load");
            return ContactsIds;
        }

        public static Dictionary<Guid, Guid> GetEntityCar(IOrganizationService service)
        {
            EntityCollection collection = EntityHelper.GetEntityCollection(service, "cds_car", "cds_carid", "cds_carclassid");
            Console.WriteLine("Get Data Car load");
            return collection.Entities.Select(x => new { CarId = x.Id, CarClassId = x.GetAttributeValue<EntityReference>("cds_carclassid").Id }).ToDictionary(x => x.CarId, x => x.CarClassId);
        }
        
        
    }
}
