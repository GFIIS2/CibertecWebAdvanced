using Cibertec.MockData;
using Cibertec.Models;
using Cibertec.UnitOfWork;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Cibertec.Repositories.Tests
{
    public class SupplierRepositoryTest
    {
        private readonly IUnitOfWork _unit;
        public SupplierRepositoryTest()
        {
            //caso 1
            //_unit = new CibertecUnitOfWork(ConfigSettings.ConnectionString);

            //caso 2
            _unit = MockedUnitOfWork.GetUnitOfWork().Object;
        }

        [Fact(DisplayName = "First Unit Test")]
        public void First_Unit_Test()
        {
            var supplierList = _unit.Suppliers.GetById(1);
            supplierList.Should().NotBeNull();
        }

        [Fact(DisplayName = "[SupplierRepositoryTests] Get All Suppliers")]
        public void Suppliers_Get_All()
        {
            //Forma 1
            //var customerList = _unit.Customers.GetAll().ToList();
            //Assert.NotNull(customerList);
            //Assert.True(result.customerList() > 0);

            //Forma 2
            var supplierList = _unit.Suppliers.GetAll().ToList();
            supplierList.Count.Should().BeGreaterThan(0);
            supplierList.Count.Should().Be(3);
        }

        [Fact(DisplayName = "Supplier GetById Test")]
        public void Get_By_Id()
        {
            var result = _unit.Suppliers.GetById(1);
            //forma 1
            Assert.NotNull(result);
            Assert.True(result.Id > 0);

            //forma 2            
            //result.Should().BeGreaterThan(0);
        }

        [Fact(DisplayName = "Supplier Insert Test")]
        public void Insert_Supplier()
        {
            var result = _unit.Suppliers.Insert(new Supplier());
            //Forma 1
            //Assert.True(result > 0);

            //Forma 2
            result.Should().BeGreaterThan(0);
        }

        [Fact(DisplayName = "Supplier Insert Wrong")]
        public void Insert_Supplier_Wrong()
        {
            var result = _unit.Suppliers.Insert(new Supplier());
            //Forma 1
            Assert.True(result > 0);

            //Forma 2
            //result.Should().Be(0);
        }

        [Fact(DisplayName = "Supplier Update Test")]
        public void Supplier_Update()
        {
            var result = _unit.Suppliers.Update(new Supplier());
            //Forma 1
            //Assert.True(result);

            //forma 2
            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Supplier Delete Test")]
        public void Supplier_Delete()
        {
            var result = _unit.Suppliers.Delete(new Supplier());
            //forma 1
            //Assert.True(result);

            //forma 2
            result.Should().BeTrue();
        }


        [Theory(DisplayName = "Display Search By Names Test")]
        //[InlineData("Gustavo")]
        //[InlineData("Julio")]
        //[InlineData("Alan", "Garcia")]
        public void Supplier_SearchByName(string contactName)
        {
            var suppier = _unit.Suppliers.SearchByNames(contactName);
            suppier.Should().NotBeNull();
        }
    }
}
