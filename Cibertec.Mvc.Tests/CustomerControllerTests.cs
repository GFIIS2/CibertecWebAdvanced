using Cibertec.Web.Controllers;
using Cibertec.MockData;
using System.Linq;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Cibertec.Models;

namespace Cibertec.Mvc.Tests
{
    public class CustomerControllerTests
    {
        private readonly CustomerController _controller;
        public CustomerControllerTests()
        {
            _controller = new CustomerController(MockedUnitOfWork.GetUnitOfWork().Object);
        }

        [Fact(DisplayName ="CustomerController Index Test")]
        public void CustomerControllerTest()
        {
            var result = _controller.Index() as ViewResult;
            result.Should().NotBeNull();
            var model = result.Model as List<Customer>;
            model.Count().Should().Be(2);
            model[0].Id.Should().Be(1);
        }
    }
}
