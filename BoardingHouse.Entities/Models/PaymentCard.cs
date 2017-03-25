using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardingHouse.Entities.Models
{
    [Table("PaymentCard")]
    public class PaymentCard
    {
        public int PaymentCardID { get; set; }
        [StringLength(256)]
        public string ISP { get; set; }
        [StringLength(256)]
        public string Card { get; set; }
        [StringLength(256)]
        public string Seri { get; set; }
        public bool? Status { get; set; }
        public string UserID { get; set; }
    }
}
