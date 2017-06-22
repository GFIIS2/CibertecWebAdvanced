using System;
using System.Collections.Generic;

namespace Cibertec.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
