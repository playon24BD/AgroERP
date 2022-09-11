using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.ControlPanel.DTOModels
{
   public class ModuleDTO
    {
        public long MId { get; set; }
        public string ModuleName { get; set; }
        public string IconName { get; set; }
        public string IconColor { get; set; }
        public long? EUserId { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public long? UpUserId { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public string EntryUser { get; set; }
        public string UpdateUser { get; set; }
    }
}
