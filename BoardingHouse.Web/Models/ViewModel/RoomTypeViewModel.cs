using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoaringHouse.Web.Models.ViewModel
{
    public class RoomTypeViewModel
    {
        public int RoomTypeID { get; set; }

        public string RoomTypeName { get; set; }

        public string Alias { get; set; }

        public string Description { get; set; }

        public int? ParentID { get; set; }

        public int? DisplayOrder { get; set; }

        public string Image { get; set; }

        public bool? HomeFlag { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string MetaKeyword { get; set; }

        public string MetaDescription { get; set; }

        public bool Status { get; set; }
    }
}