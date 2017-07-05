﻿using System;
using System.Collections.Generic;
using System.Text;
using Cibertec.Models;
using Cibertec.Repositories;
using Cibertec.Repositories.Northwind;
using Cibertec.Repositories.Northwind.Dapper;

namespace Cibertec.UnitOfWork
{
    public class CibertecUnitOfWork : IUnitOfWork
    {
        public CibertecUnitOfWork(string connectionString)
        {
            Customers = new CustomerRepository(connectionString);
            Products = new ProductRepository(connectionString);
            Suppliers = new SupplierRepository(connectionString);
            Orders = new OrderRepository(connectionString);
            OrderItems = new OrderItemRepository(connectionString);
            Users = new UserRepository(connectionString);
            //Customers = new Repository<Customer>(connectionString);
            //Products = new RepositoryDapper<Product>(connectionString);
            //Suppliers = new RepositoryDapper<Supplier>(connectionString);
            //Orders = new RepositoryDapper<Order>(connectionString);
            //OrderItems = new RepositoryDapper<OrderItem>(connectionString);
        }

        public ICustomerRepository Customers { get; private set; }
        public IProductRepository Products { get; private set; }
        public ISupplierRepository Suppliers { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IOrderItemRepository OrderItems { get; private set; }
        public IUserRepository Users { get; private set; }
        //public IRepository<Customer> Customers { get; private set; }
        //public IRepository<Product> Products { get; private set; }
        //public IRepository<Supplier> Suppliers { get; private set; }
        //public IRepository<Order> Orders { get; private set; }
        //public IRepository<OrderItem> OrderItems { get; private set; }        
    }
}
