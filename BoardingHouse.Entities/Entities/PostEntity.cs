using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardingHouse.Entities.Entities
{
    public class PostEntity
    {
        public int PostID { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }

        public int PostTypeID { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public bool HomeFlag { get; set; }

        public bool HotFlag { get; set; }

        public int? ViewCount { get; set; }
        public string MoreImages { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ExpireDate { get; set; }

        public bool Status { get; set; }

        public string Tags { get; set; }
        public string CreatedBy { get; set; }
    }
}
