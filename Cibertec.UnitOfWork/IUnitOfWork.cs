using Cibertec.Models;
using Cibertec.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cibertec.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Customer> Customers { get; }
        IRepository<Product> Products { get; }
        IRepository<Supplier> Suppliers { get; }
        IRepository<Order> Orders { get; }
        IRepository<OrderItem> OrderItems { get; }
    }
}
