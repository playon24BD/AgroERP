using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    [Table("tblMeasurement")]
    public class MeasurementSetup
    {
        [Key]
        public long MeasurementId { get; set; }
        public string MeasurementName { get; set; }
        public long OrganizationId { get; set; }
        public int MasterCarton { get; set; }
        public int InnerBox { get; set; }
        public double PackSize { get; set; }
        public long UnitId { get; set; }
        public long RoleId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long UpdateUserId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long EntryUserId { get; set; }
        public string Status { get; set; }
        public double UnitKG { get; set; }
        public long FinishGoodProductId { get; set; }
    }
}
