using System.Collections.Generic;

namespace Cibertec.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public decimal UnitPrice { get; set; }
        public string Package { get; set; }
        public bool IsDiscontinued { get; set; }

        //public virtual Supplier Supplier { get; set; }
        //public virtual IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
