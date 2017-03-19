using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoaringHouse.API.Models.ViewModel
{
    public class WardViewModel
    {
        public int wardid { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public string location { get; set; }

        public int? districtid { get; set; }
    }
}