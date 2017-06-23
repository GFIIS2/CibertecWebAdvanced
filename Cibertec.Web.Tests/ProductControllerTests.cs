using Cibertec.MockData;
using Cibertec.Models;
using Cibertec.Web.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cibertec.Web.Tests
{
    public class ProductControllerTests
    {
        private readonly ProductController _productos;
        public ProductControllerTests()
        {
            //caso 1
            //_unit = new CibertecUnitOfWork(ConfigSettings.ConnectionString);

            //caso 2

            _productos = new ProductController(MockedUnitOfWork.GetUnitOfWork());
        }

        [Fact(DisplayName = "ProductControler Index Test")]
        public void ProductController_Index()
        {
            //FORMA 2
            var result = _productos.Index() as ViewResult;
            result.Should().NotBeNull();
            var model = result.Model as List<Product>;
            model.Count().Should().Be(2);
            model[0].Id.Should().Be(1);


            //FORMA 1
            //var customerList = _customer.Index() as ViewResult;
            //(customerList.Model as IEnumerable<Customer>).Count().Should().Be(3);

        }

       
    }
}
