namespace Cibertec.Models
{
    public class Customer
    {
        public int Id { get; set; }
        
        //public String MyProperty { get; set; } //es un objeto "String" por eso no es recomendable el uso.

        public string FirstName { get; set; } //es un clase natibo por eso es recomendable el uso.
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

    }
}
