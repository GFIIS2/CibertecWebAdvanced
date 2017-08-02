using Cibertec.Models;
using System.Collections.Generic;

namespace Cibertec.Repositories.Northwind
{
    public interface IProductRepository : IRepository<Product>
    {
        Product GetByProductName(string productName);
        IEnumerable<Product> GetProductsByPagination(int startRow, int endRow);
        int RowNumbers();
    }
}
