using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime GetDate { get; set; }
        [StringLength(30), Required]
        public string Description { get; set; }
        public int CustomerId { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal Total { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual IEnumerable<Orderline> Orderlines { get; set; }
        public Order() { }

    }
}
