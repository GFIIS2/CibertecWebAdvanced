using Cibertec.Models;
using Dapper;
using System.Data.SqlClient;

namespace Cibertec.Repositories.Northwind.Dapper
{
    public class CustomerRepository : RepositoryDapper<Customer>, ICustomerRepository
    {
        public CustomerRepository(string connectionString) : base(connectionString)
        {

        }

        public Customer SearchByNames(string firstName, string lastName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@firstName", firstName);
                parameters.Add("@lastName", lastName);

                return connection.QueryFirst<Customer>("dbo.SearchByNames", 
                    parameters, 
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
