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
    public class OrderControllerTests
    {
        private readonly OrderController _order;
        public OrderControllerTests()
        {
            //caso 1
            //_unit = new CibertecUnitOfWork(ConfigSettings.ConnectionString);

            //caso 2

            _order = new OrderController(MockedUnitOfWork.GetUnitOfWork());
        }

        [Fact(DisplayName = "OrderControler Index Test")]
        public void OrderController_Index()
        {
            //FORMA 2
            var result = _order.Index() as ViewResult;
            result.Should().NotBeNull();
            var model = result.Model as List<Order>;
            model.Count().Should().Be(2);
            model[0].Id.Should().Be(1);


            //FORMA 1
            //var customerList = _customer.Index() as ViewResult;
            //(customerList.Model as IEnumerable<Customer>).Count().Should().Be(3);

        }
    }
}
