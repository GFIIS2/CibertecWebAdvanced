using Cibertec.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cibertec.Repositories.Northwind
{
    public interface IProductRepository : IRepository<Product>
    {
        Product SearchByNames(string productName);
    }
}
