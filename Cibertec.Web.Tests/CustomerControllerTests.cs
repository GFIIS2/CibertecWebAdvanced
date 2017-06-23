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
    public class CustomerControllerTests
    {        
        private readonly CustomerController _customer;
        public CustomerControllerTests()
        {
            //caso 1
            //_unit = new CibertecUnitOfWork(ConfigSettings.ConnectionString);

            //caso 2
            
            _customer = new CustomerController(MockedUnitOfWork.GetUnitOfWork());
        }

        [Fact(DisplayName = "CustomerControler Index Test")]
        public void CustomerController_Index()
        {
            //FORMA 2
            var result = _customer.Index() as ViewResult;
            result.Should().NotBeNull();
            var model = result.Model as List<Customer>;
            model.Count().Should().Be(2);
            model[0].Id.Should().Be(1);

                       
            //FORMA 1
            //var customerList = _customer.Index() as ViewResult;
            //(customerList.Model as IEnumerable<Customer>).Count().Should().Be(3);
            
        }

        [Fact(DisplayName = "CustomerControler Detail Test")]
        public void CustomerController_Detail()
        {
            var customerList = _customer.Detail() as ViewResult;
            (customerList.Model as Customer).Should().NotBeNull();

        }
    }
}
