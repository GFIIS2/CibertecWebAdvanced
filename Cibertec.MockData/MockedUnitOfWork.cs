using Cibertec.Models;
using Cibertec.UnitOfWork;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Cibertec.MockData
{
    public static class MockedUnitOfWork
    {
        //public static IUnitOfWork GetUnitOfWork()
        //{
        //    Mock<IUnitOfWork> unit = new Mock<IUnitOfWork>();
        //    unit.ConfigureCustomer();
        //    return unit.Object;
        //}

        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            Mock<IUnitOfWork> unit = new Mock<IUnitOfWork>();
            unit.ConfigureCustomer();
            unit.ConfigureProduct();
            unit.ConfigureSupplier();
            return unit;
        }
    }

    public static class MockedUnitOfWorkExtensions
    {
        public static Mock<IUnitOfWork> ConfigureCustomer(this Mock<IUnitOfWork> mock)
        {
            var customerList = new List<Customer>
                {
                    new Customer
                    {
                        Id=1,
                        City = "Lima",
                        Country = "Peru",
                        FirstName = "Gustavo",
                        LastName = "Yauri",
                        Orders = new List<Order>(),
                        Phone = "555-55555"
                    },
                    new Customer
                    {
                        Id=2,
                        City = "Lima",
                        Country = "Peru",
                        FirstName = "Juan",
                        LastName = "Perez",
                        Orders = new List<Order>(),
                        Phone="555-66666"
                    },
                    new Customer
                    {
                        Id=3,
                        City = "Lima",
                        Country = "Peru",
                        FirstName = "Julio",
                        LastName = "Velarde",
                        Orders = new List<Order>(),
                        Phone="555-66688"
                    }
                };


            mock.Setup(c => c.Customers.GetAll()).Returns(customerList);
            mock.Setup(c => c.Customers.Insert(It.IsAny<Customer>())).Returns(1);
            mock.Setup(c => c.Customers.Update(It.IsAny<Customer>())).Returns(true);
            mock.Setup(c => c.Customers.Delete(It.IsAny<Customer>())).Returns(true);
            mock.Setup(c => c.Customers.SearchByNames(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string firstName, string LastName) =>
                {
                    return customerList.FirstOrDefault(x => x.FirstName == firstName && x.LastName == LastName);
                });

            mock.Setup(c => c.Customers.GetById(It.IsAny<int>()))
                .Returns((int id) =>
                {
                    return customerList.FirstOrDefault(x => x.Id == id);
                });
            return mock;
        }
        public static Mock<IUnitOfWork> ConfigureProduct(this Mock<IUnitOfWork> mock)
        {
            var productList = new List<Product>
                {
                    new Product
                    {
                        Id = 1,
                        ProductName = "PC",
                        SupplierId = 1,
                        UnitPrice = 15.25M,
                        Package = "Tecnologia",
                        IsDiscontinued = false,
                    },
                    new Product
                    {
                        Id = 2,
                        ProductName = "LAPTOP",
                        SupplierId = 1,
                        UnitPrice = 165.25M,
                        Package = "Tecnologia",
                        IsDiscontinued = false
                    },
                    new Product
                    {
                        Id = 3,
                        ProductName = "MOUSE",
                        SupplierId = 1,
                        UnitPrice = 55.25M,
                        Package = "Tecnologia",
                        IsDiscontinued = false
                    }
                };
            mock.Setup(c => c.Products.GetAll()).Returns(productList);
            mock.Setup(c => c.Products.Insert(It.IsAny<Product>())).Returns(1);
            mock.Setup(c => c.Products.Update(It.IsAny<Product>())).Returns(true);
            mock.Setup(c => c.Products.Delete(It.IsAny<Product>())).Returns(true);
            mock.Setup(c => c.Products.SearchByNames(It.IsAny<string>()))
                .Returns((string productName) =>
                {
                    return productList.FirstOrDefault(x => x.ProductName == productName);
                });
            mock.Setup(c => c.Products.GetById(It.IsAny<int>()))
                .Returns((int id) =>
                {
                    return productList.FirstOrDefault(x => x.Id == id);
                });
            return mock;
        }

        public static Mock<IUnitOfWork> ConfigureSupplier(this Mock<IUnitOfWork> mock)
        {
            var supplierList = new List<Supplier>
                {
                    new Supplier
                    {
                        Id = 1,
                        ContactName = "PC",
                        ContactTitle = "1",
                        City = "Lima",
                        Country = "Peru",
                        CompanyName = "BF",
                        Fax = "102",
                        Phone="123"
                    },
                    new Supplier
                    {
                        Id = 2,
                        ContactName = "PC",
                        ContactTitle = "1",
                        City = "Lima",
                        Country = "Peru",
                        CompanyName = "BF",
                        Fax = "102",
                        Phone="123"
                    },
                    new Supplier
                    {
                        Id = 3,
                        ContactName = "PC",
                        ContactTitle = "1",
                        City = "Lima",
                        Country = "Peru",
                        CompanyName = "BF",
                        Fax = "102",
                        Phone="123"
                    }
                };
            mock.Setup(c => c.Suppliers.GetAll()).Returns(supplierList);
            mock.Setup(c => c.Suppliers.Insert(It.IsAny<Supplier>())).Returns(1);
            mock.Setup(c => c.Suppliers.Update(It.IsAny<Supplier>())).Returns(true);
            mock.Setup(c => c.Suppliers.Delete(It.IsAny<Supplier>())).Returns(true);
            mock.Setup(c => c.Suppliers.SearchByNames(It.IsAny<string>()))
                .Returns((string contactName) =>
                {
                    return supplierList.FirstOrDefault(x => x.ContactName == contactName);
                });
            mock.Setup(c => c.Suppliers.GetById(It.IsAny<int>()))
                .Returns((int id) =>
                {
                    return supplierList.FirstOrDefault(x => x.Id == id);
                });
            return mock;
        }
    }
}
