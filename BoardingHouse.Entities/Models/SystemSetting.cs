namespace BoardingHouse.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SystemSetting")]
    public partial class SystemSetting
    {
        public int ID { get; set; }

        [StringLength(250)]
        public string Field { get; set; }

        [StringLength(250)]
        public string Value { get; set; }

        [StringLength(250)]
        public string ValDefault { get; set; }
    }
}
