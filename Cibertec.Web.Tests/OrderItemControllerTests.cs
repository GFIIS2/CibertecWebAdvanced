﻿using Cibertec.MockData;
using Cibertec.Models;
using Cibertec.Web.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cibertec.Web.Tests
{
    public class OrderItemControllerTests
    {
        private readonly OrderItemController _orderItem;
        public OrderItemControllerTests()
        {
            //caso 1
            //_unit = new CibertecUnitOfWork(ConfigSettings.ConnectionString);

            //caso 2

            _orderItem = new OrderItemController(MockedUnitOfWork.GetUnitOfWork());
        }

        [Fact(DisplayName = "OrderItemControler Index Test")]
        public void OrderItemController_Index()
        {
            //FORMA 2
            var result = _orderItem.Index() as ViewResult;
            result.Should().NotBeNull();
            var model = result.Model as List<OrderItem>;
            model.Count().Should().Be(2);
            model[0].Id.Should().Be(1);


            //FORMA 1
            //var customerList = _customer.Index() as ViewResult;
            //(customerList.Model as IEnumerable<Customer>).Count().Should().Be(3);

        }
    }
}
