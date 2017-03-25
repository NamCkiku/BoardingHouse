namespace BoardingHouse.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Room")]
    public partial class Room
    {
        public int RoomID { get; set; }

        [Required]
        [StringLength(256)]
        public string RoomName { get; set; }

        [Required]
        [StringLength(256)]
        public string Alias { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required]
        [StringLength(256)]
        public string Address { get; set; }

        public int RoomTypeID { get; set; }

        [Required]
        [StringLength(256)]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImages { get; set; }

        public int? WardID { get; set; }

        public int? DistrictID { get; set; }

        public int? ProvinceID { get; set; }

        public double? Acreage { get; set; }

        public decimal Price { get; set; }

        public int? MoreInfomationID { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ExpireDate { get; set; }

        public int? ViewCount { get; set; }

        public bool Status { get; set; }

        public double? Lat { get; set; }

        public double? Lng { get; set; }
        [Required]
        [StringLength(256)]
        public string FullName { get; set; }
        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        public int? UserID{ get; set; }
    }
}
