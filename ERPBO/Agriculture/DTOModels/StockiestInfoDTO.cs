using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DTOModels
{
    public class StockiestInfoDTO
    {
        public long StockiestId { get; set; }
        public string StockiestName { get; set; }
        public long OrganizationId { get; set; }
        public long TerritoryId { get; set; }
        public long RoleId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public string Status { get; set; }
        public string TerritoryName { get; set; }


        public string StockiestPhoneNumber { get; set; }
        public string StockiestMail { get; set; }
        public string StockiestTradeLicense { get; set; }
        public string StockiestNID { get; set; }
        public string StockiestContactPerson { get; set; }
        public string StockiestContactPersonPHNumber { get; set; }
        public long AreaId { get; set; }
        public double CreditLimit { get; set; }
        public string AreaName { get; set; }
    }
}
