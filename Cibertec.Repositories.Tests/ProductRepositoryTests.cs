using Cibertec.MockData;
using Cibertec.Models;
using Cibertec.UnitOfWork;
using FluentAssertions;
using System.Linq;
using Xunit;


namespace Cibertec.Repositories.Tests
{
    public class ProductRepositoryTests
    {
        private readonly IUnitOfWork _unit;
        public ProductRepositoryTests()
        {
            //caso 1
            //_unit = new CibertecUnitOfWork(ConfigSettings.ConnectionString);

            //caso 2
            _unit = MockedUnitOfWork.GetUnitOfWork().Object;
        }

        [Fact(DisplayName = "First Unit Test")]
        public void First_Unit_Test()
        {
            var productList = _unit.Products.GetById(1);
            productList.Should().NotBeNull();
        }

        [Fact(DisplayName = "Product Get All Test")]
        public void Products_Get_All()
        {
            //Forma 1
            //var customerList = _unit.Customers.GetAll().ToList();
            //Assert.NotNull(customerList);
            //Assert.True(result.customerList() > 0);

            //Forma 2
            var productList = _unit.Products.GetAll().ToList();
            productList.Count.Should().BeGreaterThan(0);
            productList.Count.Should().Be(3);
        }

        [Fact(DisplayName = "Product GetById Test")]
        public void Get_By_Id()
        {
            var result = _unit.Products.GetById(1);
            //forma 1
            Assert.NotNull(result);
            Assert.True(result.Id > 0);

            //forma 2            
            //result.Should().BeGreaterThan(0);
        }

        [Fact(DisplayName = "Product Insert Test")]
        public void Insert_Product()
        {
            var result = _unit.Products.Insert(new Product());
            //Forma 1
            //Assert.True(result > 0);

            //Forma 2
            result.Should().BeGreaterThan(0);
        }

        [Fact(DisplayName = "Product Insert Wrong")]
        public void Insert_Product_Wrong()
        {
            var result = _unit.Products.Insert(new Product());
            //Forma 1
            Assert.True(result > 0);

            //Forma 2
            //result.Should().Be(0);
        }

        [Fact(DisplayName = "Product Update Test")]
        public void Product_Update()
        {
            var result = _unit.Products.Update(new Product());
            //Forma 1
            //Assert.True(result);

            //forma 2
            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Product Delete Test")]
        public void Product_Delete()
        {
            var result = _unit.Products.Delete(new Product());
            //forma 1
            //Assert.True(result);

            //forma 2
            result.Should().BeTrue();
        }


        [Theory(DisplayName = "Product Search By Product Name Test")]
        [InlineData("Gustavo", "Yauri")]
        [InlineData("Julio", "Velarde")]
        //[InlineData("Alan", "Garcia")]
        public void Product_SearchByName(string productName)
        {
            var customer = _unit.Products.SearchByNames(productName);
            customer.Should().NotBeNull();
        }
    }
}
