namespace BoardingHouse.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Post")]
    public partial class Post
    {
        public int PostID { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        public string Alias { get; set; }

        public int PostTypeID { get; set; }

        [StringLength(256)]
        public string Image { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string Content { get; set; }

        public bool HomeFlag { get; set; }

        public bool HotFlag { get; set; }

        public int? ViewCount { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImages { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ExpireDate { get; set; }

        public bool Status { get; set; }

        public string Tags { get; set; }

        [StringLength(250)]
        public string CreatedBy { get; set; }
    }
}
