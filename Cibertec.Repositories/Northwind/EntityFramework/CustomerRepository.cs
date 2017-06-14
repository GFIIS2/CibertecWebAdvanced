using Cibertec.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Cibertec.Repositories.Northwind.EntityFramework
{
    public class CustomerRepository : RepositoryEF<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext dbContext) : base(dbContext)
        {

        }

        public Customer SearchByNames(string firstName, string lastName)
        {
            return _dbContext.Set<Customer>().FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName);
                
        }
    }
}
