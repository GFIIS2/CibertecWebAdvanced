using Microsoft.AspNetCore.Mvc;
using Cibertec.UnitOfWork;
using Cibertec.Models;
using Microsoft.AspNetCore.Authorization;

namespace Cibertec.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("customer")]
    [Authorize]
    public class CustomerController : BaseController
    {
        public CustomerController(IUnitOfWork unit) : base(unit)
        {
        }

        //private readonly IUnitOfWork _unit;
        //public CustomerController(IUnitOfWork unit)
        //{
        //    _unit = unit;
        //}

        [HttpGet]
        [Route("list")]
        public IActionResult List()
        {
            return Ok(_unit.Customers.GetAll());
        }

        [HttpPost]
        public IActionResult Create([FromBody]Customer customer)
        {
            return Ok(_unit.Customers.Insert(customer));
        }
    }
}