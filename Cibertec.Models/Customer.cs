using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cibertec.Models
{
    public class Customer
    {
        public int Id { get; set; }
        
        //public String MyProperty { get; set; } //es un objeto "String" por eso no es recomendable el uso.

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "this field is required")]
        public string FirstName { get; set; } //es un clase natibo por eso es recomendable el uso.
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "this field is required")]
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

        public virtual IEnumerable<Order> Orders { get; set; }
    }
}
