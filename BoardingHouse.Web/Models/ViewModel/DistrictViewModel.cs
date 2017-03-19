using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardingHouse.Web.Models.ViewModel
{
    public class DistrictViewModel
    {
        public int districtid { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public string location { get; set; }

        public int? provinceid { get; set; }
    }
}