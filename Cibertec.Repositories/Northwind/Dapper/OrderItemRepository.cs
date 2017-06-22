using Cibertec.Models;
using Dapper;
using System.Data.SqlClient;

namespace Cibertec.Repositories.Northwind.Dapper
{
    public class OrderItemRepository : RepositoryDapper<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(string connectionString) : base(connectionString)
        {
        }

        public OrderItem SearchByUnitPrice(decimal unitPrice)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@unitPrice", unitPrice);

                return connection.QueryFirst<OrderItem>("dbo.OrdeItem_SearchByUnitPrice",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
