using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.ControlPanel.ViewModels
{
   public class MainMenuViewModel
    {
        public long MMId { get; set; }
        [StringLength(100)]
        public string MenuName { get; set; }
        [Range(1,long.MaxValue)]
        public long? EUserId { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        [Range(1, long.MaxValue)]
        public long? UpUserId { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        [Range(1, long.MaxValue)]
        public long MId { get; set; }

        //custom
        [StringLength(100)]
        public string ModuleName { get; set; }
        public string EntryUser { get; set; }
        public string UpdateUser { get; set; }
    }
}
