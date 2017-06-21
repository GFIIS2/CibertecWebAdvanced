using Cibertec.Models;
using Cibertec.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            _unit = MockedUnitOfWork.GetUnitOfWork();
        }

        [Fact(DisplayName = "[CustomerRepositoryTests] Get All Customers")]
        public void Get_All_Customers()
        {
            var result = _unit.Customers.GetAll();

            //Forma 1
            Assert.NotNull(result);
            Assert.True(result.Count() > 0);

            //Forma 2
            result.Should().NotBeNull();
            result.Count().Should().BeGraterThan(0);
        }

        [Fact(DisplayName = "[CustomerRepositoryTests] Get By Id")]
        public void Get_By_Id()
        {
            var result = _unit.Customers.GetById(1);
            //forma 1
            Assert.NotNull(result);
            Assert.True(result.Id > 0);

            //forma 2
            result.Count().Should().BeGraterThan(0);
        }

        [Fact(DisplayName = "[CustomerRepositoryTests] Insert")]
        public void Insert_Customer()
        {
            var result = _unit.Customers.Insert(null);
            //Forma 1
            Assert.True(result > 0);

            //Forma 2
            result.Count().Should().BeGraterThan(0);
        }

        [Fact(DisplayName = "[CustomerRepositoryTests] Insert")]
        public void Insert_Customer_Wrong()
        {
            var result = _unit.Customers.Insert(new Customer());
            //Forma 1
            Assert.True(result > 0);

            //Forma 2
            result.Should().Be(0);
        }

        [Fact(DisplayName = "[CustomerRepositoryTests] Update")]
        public void Update_Customer()
        {
            var result = _unit.Customers.Update(null);
            //Assert.NotNull(result);
            Assert.True(result);
        }

        [Fact(DisplayName = "[CustomerRepositoryTests] Delete")]
        public void Delete_Customer()
        {
            var result = _unit.Customers.Delete(null);
            //Assert.NotNull(result);
            Assert.True(result);
        }
    }
}
