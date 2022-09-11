using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.ControlPanel.DomainModels
{
    [Table("tblMeasurementInfo")]
    public class MeasurementSetup
    {
        [Key]
        public long MeasurementId { get; set; }
        public string MeasurementName { get; set; }
        public long OrganizationId { get; set; }
        public long MasterCarton { get; set; }
        public long InnerBox { get; set; }
        public long PackSize { get; set; }
        public string Unit { get; set; }
        public long RoleId { get; set; }
        public DateTime UpdateDate { get; set; }
        public long UpdateUserId { get; set; }
        public DateTime EntryDate { get; set; }
        public long EntryUserId { get; set; }
        public string Status { get; set; }
    }
}
