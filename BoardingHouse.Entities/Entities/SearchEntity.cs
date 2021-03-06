﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardingHouse.Entities.Entities
{
    public class SearchEntity
    {
        public string Keywords { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Status { get; set; }
        public int? RoomType { get; set; }
        public int? Province { get; set; }
        public int? District { get; set; }
        public int? Ward { get; set; }
    }
}
