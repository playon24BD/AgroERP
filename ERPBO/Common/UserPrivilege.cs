using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Common
{
    public class UserPrivilege
    {
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Detail { get; set; }
        public bool Delete { get; set; }
        public bool Approval { get; set; }
        public bool Report { get; set; }
    }
}
