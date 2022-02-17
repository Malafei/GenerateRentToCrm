using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGenerator
{
    public class CarTransferReport
    {
        private Guid CarTransferReportId { get; set; }
        private EntityReference CarId { get; set; }
        private bool TypeReport { get; set; }
        private bool IsDamages { get; set; }
        private DateTime DateReport { get; set; }
        private string DamageDesription { get; set; }

        public CarTransferReport(EntityReference referenceCar, bool typeReport, DateTime dateReport)
        {
            CarTransferReportId = Guid.NewGuid();
            CarId = referenceCar;
            IsDamages = GenerateCarTransferReport.GenereteDamageReport();
            if(IsDamages)
                DamageDesription = "Damage";
            TypeReport = typeReport;
            DateReport = dateReport;
        }

        public Guid PushCarTransferReportToCrm(IOrganizationService service)
        {
            Entity reportPickup = new Entity("cds_cartransferreport");
            reportPickup["cds_cartransferreportid"] = CarTransferReportId;
            reportPickup["cds_carid"] = CarId;
            reportPickup["cds_type"] = TypeReport;
            reportPickup["cds_date"] = DateReport;
            reportPickup["cds_damages"] = IsDamages;
            reportPickup["cds_damagedescription"] = DamageDesription;
            if (TypeReport)
                reportPickup["cds_name"] = "Return";
            else
                reportPickup["cds_name"] = "Pickup";

            return service.Create(reportPickup);
        }

    }
}
