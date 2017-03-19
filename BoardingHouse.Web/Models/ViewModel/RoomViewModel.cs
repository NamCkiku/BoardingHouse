using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoaringHouse.Web.Models.ViewModel
{
    public class RoomViewModel
    {
        public int RoomID { get; set; }

        public string RoomName { get; set; }

        public string Alias { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public int RoomTypeID { get; set; }
        public string RoomTypeName { get; set; }
        public string Image { get; set; }

        public string MoreImages { get; set; }

        public int WardID { get; set; }

        public int DistrictID { get; set; }

        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }

        public double? Acreage { get; set; }

        public decimal Price { get; set; }

        public int MoreInfomationID { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime ExpireDate { get; set; }

        public int ViewCount { get; set; }

        public bool Status { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }
    }
}