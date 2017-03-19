using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardingHouse.Entities.Entities
{
    public class MoreInfomationEntity
    {
        public int MoreInfomationID { get; set; }

        public string FloorNumber { get; set; }

        public string ToiletNumber { get; set; }

        public string BedroomNumber { get; set; }

        public string Compass { get; set; }

        public int? ElectricPrice { get; set; }

        public int? WaterPrice { get; set; }

        public string Convenient { get; set; }
    }
}
