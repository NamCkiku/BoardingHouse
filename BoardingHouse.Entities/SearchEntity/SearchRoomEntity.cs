using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardingHouse.Entities.SearchEntity
{
    public class SearchRoomEntity
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
