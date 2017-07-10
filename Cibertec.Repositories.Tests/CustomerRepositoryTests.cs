using Cibertec.MockData;
using Cibertec.Models;
using Cibertec.UnitOfWork;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Cibertec.Repositories.Tests
{
    public class CustomerRepositoryTests
    {
        private readonly IUnitOfWork _unit;
        public CustomerRepositoryTests()
        {
            //caso 1
            //_unit = new CibertecUnitOfWork(ConfigSettings.ConnectionString);

            //caso 2
            _unit = MockedUnitOfWork.GetUnitOfWork().Object;
        }

        [Fact(DisplayName = "First Unit Test")]
        public void First_Unit_Test()
        {
            var customerList = _unit.Customers.GetById(1);
            customerList.Should().NotBeNull() ;
        }

        [Fact(DisplayName = "[CustomerRepositoryTests] Get All Customers")]
        public void Customers_Get_All()
        {
            //Forma 1
            //var customerList = _unit.Customers.GetAll().ToList();
            //Assert.NotNull(customerList);
            //Assert.True(result.customerList() > 0);

            //Forma 2
            var customerList = _unit.Customers.GetAll().ToList();
            customerList.Count.Should().BeGreaterThan(0);
            customerList.Count.Should().Be(3);
        }

        [Fact(DisplayName = "Customer GetById Test")]
        public void Get_By_Id()
        {
            var result = _unit.Customers.GetById(1);
            //forma 1
            Assert.NotNull(result);
            Assert.True(result.Id > 0);

            //forma 2            
            //result.Should().BeGreaterThan(0);
        }

        [Fact(DisplayName = "Customer Insert Test")]
        public void Insert_Customer()
        {
            var result = _unit.Customers.Insert(new Customer());
            //Forma 1
            //Assert.True(result > 0);

            //Forma 2
            result.Should().BeGreaterThan(0);
        }

        [Fact(DisplayName = "Customer Insert Wrong")]
        public void Insert_Customer_Wrong()
        {
            var result = _unit.Customers.Insert(new Customer());
            //Forma 1
            Assert.True(result > 0);

            //Forma 2
            //result.Should().Be(0);
        }

        [Fact(DisplayName = "Customer Update Test")]
        public void Customer_Update()
        {
            var result = _unit.Customers.Update(new Customer());
            //Forma 1
            //Assert.True(result);

            //forma 2
            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Customer Delete Test")]
        public void Customer_Delete()
        {
            var result = _unit.Customers.Delete(new Customer());
            //forma 1
            //Assert.True(result);

            //forma 2
            result.Should().BeTrue();
        }


        [Theory(DisplayName = "Display Search By Names Test")]
        [InlineData("Gustavo", "Yauri")]
        [InlineData("Julio", "Velarde")]
        //[InlineData("Alan", "Garcia")]
        public void Customer_SearchByName(string firstName, string lastName)
        {
            var customer = _unit.Customers.SearchByNames(firstName, lastName);
            customer.Should().NotBeNull();
        }
    }
}
