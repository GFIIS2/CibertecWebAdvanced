using Cibertec.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Cibertec.Repositories.Northwind.Dapper
{
    public class ProductRepository : RepositoryDapper<Product>, IProductRepository
    {
        public ProductRepository(string connectionString) : base(connectionString)
        {
        }
        
        public Product SearchByNames(string productName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@productName", productName);
                
                return connection.QueryFirst<Product>("dbo.Prod_SearchByNames",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
