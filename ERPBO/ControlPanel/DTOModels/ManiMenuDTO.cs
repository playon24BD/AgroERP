using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.ControlPanel.DTOModels
{
   public class MainMenuDTO
    {
        public long MMId { get; set; }
        public string MenuName { get; set; }
        public long? EUserId { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public long? UpUserId { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public long MId { get; set; }
        //custom
        public string ModuleName { get; set; }
        public string EntryUser { get; set; }
        public string UpdateUser { get; set; }
    }
}
