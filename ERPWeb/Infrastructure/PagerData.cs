using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPWeb.Infrastructure
{
    public class PagerData
    {
        public PagerData(int recordCount, int perPage)
        {
            this.TotalPages = (int)Math.Ceiling(recordCount / (decimal)perPage);
            this.TotalRecords = recordCount;
        }
        public int TotalPages { get; private set; }
        public int Current { get; set; }
        public int TotalRecords { get; private set; }
        public int Serial { get; set; }
    }
}