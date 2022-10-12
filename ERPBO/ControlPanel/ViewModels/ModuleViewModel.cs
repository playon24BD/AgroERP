using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.ControlPanel.ViewModels
{
   public class ModuleViewModel
    {
        public long MId { get; set; }
        [StringLength(100)]
        public string ModuleName { get; set; }
        [StringLength(100)]
        public string IconName { get; set; }
        public string IconColor { get; set; }
        [Range(1,long.MaxValue)]
        public long? EUserId { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        [Range(1, long.MaxValue)]
        public long? UpUserId { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public string EntryUser { get; set; }
        public string UpdateUser { get; set; }
    }
}
