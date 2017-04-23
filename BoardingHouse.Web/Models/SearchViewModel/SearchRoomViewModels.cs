using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardingHouse.Web.Models.SearchViewModel
{
    public class SearchRoomViewModels
    {
        public int? RoomTypeID { get; set; }
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }

        public int? WardID { get; set; }

        public int? DistrictID { get; set; }

        public int? ProvinceID { get; set; }
        public string Keywords { get; set; }
    }
}