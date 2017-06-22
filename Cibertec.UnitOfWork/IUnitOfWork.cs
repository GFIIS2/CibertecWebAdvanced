using Cibertec.Models;
using Cibertec.Repositories;
using Cibertec.Repositories.Northwind;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cibertec.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; }
        IProductRepository Products { get; }
        ISupplierRepository Suppliers { get; }
        IOrderRepository Orders { get; }
        IOrderItemRepository OrderItems { get; }
        //IRepository<Customer> Customers { get; }
        //IRepository<Product> Products { get; }
        //IRepository<Supplier> Suppliers { get; }
        //IRepository<Order> Orders { get; }
        //IRepository<OrderItem> OrderItems { get; }
    }
}
