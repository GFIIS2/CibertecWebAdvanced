using Cibertec.Models;
using System.Collections.Generic;

namespace Cibertec.Repositories.Northwind
{
    public interface IProductRepository : IRepository<Product>
    {
        Product SearchByNames(string productName);
        IEnumerable<Product> SearchByRange(int startRow, int endRow);
        int RowNumbers();
    }
}
