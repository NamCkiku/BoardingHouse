namespace BoardingHouse.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MoreInfomation")]
    public partial class MoreInfomation
    {
        public int MoreInfomationID { get; set; }

        public string FloorNumber { get; set; }

        public string ToiletNumber { get; set; }

        public string BedroomNumber { get; set; }

        public string Compass { get; set; }

        public int? ElectricPrice { get; set; }

        public int? WaterPrice { get; set; }

        [Column(TypeName = "xml")]
        public string Convenient { get; set; }
    }
}
