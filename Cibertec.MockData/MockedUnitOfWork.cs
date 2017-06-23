using Cibertec.Models;
using Cibertec.UnitOfWork;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Cibertec.MockData
{
    public class MockedUnitOfWork
    {
        public static IUnitOfWork GetUnitOfWork()
        {
            Mock<IUnitOfWork> unit = new Mock<IUnitOfWork>();
            unit.ConfigureCustomer();
            return unit.Object;
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
    }
}
