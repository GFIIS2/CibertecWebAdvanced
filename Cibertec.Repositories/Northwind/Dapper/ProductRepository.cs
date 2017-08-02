using Cibertec.Models;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Cibertec.Repositories.Northwind.Dapper
{
    public class ProductRepository : RepositoryDapper<Product>, IProductRepository
    {
        public ProductRepository(string connectionString) : base(connectionString)
        {
        }
        
        public Product GetByProductName(string productName)
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

        public int RowNumbers()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>("SELECT Count(Id) FROM dbo.Product");
            }
        }

        public IEnumerable<Product> GetProductsByPagination(int startRow, int endRow)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@startRow", startRow);
                parameters.Add("@endRow", endRow);

                return connection.Query<Product>("dbo.ProductPagedList",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
