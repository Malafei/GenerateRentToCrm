using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGenerator
{
    public class Rent
    {
        public DateTime Pickup { get; set; }
        public DateTime Return { get; set; }
        public EntityReference CarClass { get; set; }
        public EntityReference Car { get; set; }
        public EntityReference Contact { get; set; }
        public OptionSetValue PickupLocation { get; set; }
        public OptionSetValue ReturnLocation { get; set; }
        public OptionSetValue StatusCode { get; set; }
        public OptionSetValue StateCode { get; set; }
        public Money Price { get; set; }
        public bool Paid { get; set; }
        public EntityReference CarTransferPickupReport { get; set; }
        public EntityReference CarTransferReturnReport { get; set; }

        public CarTransferReport ReportToRent { get; set; }

        public Rent(List<Guid> carsClass, Dictionary<Guid, Guid> cars, List<Guid> contacts, IOrganizationService service)
        {
            Pickup = GenerateRent.GetRandomDate_Pickup(new DateTime(2019, 1, 1, 9, 0, 0), new DateTime(2020, 12, 31, 9, 0, 0));
            Return = GenerateRent.GetRandomDate_Returned(Pickup, new DateTime(2020, 12, 31, 9, 0, 0));
            CarClass = new EntityReference("cds_carclass", GenerateRent.GetRandomEntityReference(carsClass));

            Car = new EntityReference("cds_car", GenerateRent.GetRandomCar(cars, CarClass.Id));
            Contact = new EntityReference("contact", GenerateRent.GetRandomEntityReference(contacts));
            PickupLocation = GenerateRent.GetRandomOptionsPickup_Returned();
            ReturnLocation = GenerateRent.GetRandomOptionsPickup_Returned();
            StatusCode = GenerateRent.GetRandomStatusCode();
            StateCode = GenerateRent.GetStatusState(StatusCode);
            Paid = GenerateRent.GetRandomPaided(StatusCode);
            Price = GenerateRent.GetRandomCurency();
            CarTransferReport newPickupReport = new CarTransferReport(Car, false, Pickup);
            if (StatusCode.Value == 754300002) //Rent
                CarTransferPickupReport = new EntityReference("cds_cartransferreport", newPickupReport.PushCarTransferReportToCrm(service));
            else if (StatusCode.Value == 754300004) //Returned
            {
                CarTransferReport newReturnReport = new CarTransferReport(Car, true, Return);
                CarTransferPickupReport = new EntityReference("cds_cartransferreport", newPickupReport.PushCarTransferReportToCrm(service));
                CarTransferReturnReport = new EntityReference("cds_cartransferreport", newReturnReport.PushCarTransferReportToCrm(service));
            }
        }

        public Guid PushRentToCrm(IOrganizationService service)
        {
            Entity rent = new Entity("cds_rent");
            rent["cds_rentid"] = Guid.NewGuid();
            rent["cds_carclass"] = CarClass;
            rent["cds_carid"] = Car;
            rent["cds_customerid"] = Contact;
            rent["cds_pickuplocation"] = PickupLocation;

            if (PickupLocation.Value != 754300002)
                rent["cds_recervedpickup"] = Pickup;
            else
                rent["cds_actualpickup"] = Pickup;
            rent["cds_returnlocation"] = ReturnLocation;

            if (ReturnLocation.Value != 754300002)
                rent["cds_recervedhandover"] = Return;
            else
                rent["cds_actualreturn"] = Return;

            if (StatusCode.Value <= 754300002)
                rent["statecode"] = new OptionSetValue(0);
            else
                rent["statecode"] = new OptionSetValue(1);
            rent["statuscode"] = StatusCode;
            rent["cds_paid"] = Paid;
            rent["cds_price"] = Price;

            if (StatusCode.Value == 754300002)
                rent["cds_carpickupreport"] = CarTransferPickupReport;
            else if (StatusCode.Value == 754300004)
            {
                rent["cds_carpickupreport"] = CarTransferPickupReport;
                rent["cds_carreturnreport"] = CarTransferReturnReport;
            }


            return service.Create(rent);
        }

    }
}
