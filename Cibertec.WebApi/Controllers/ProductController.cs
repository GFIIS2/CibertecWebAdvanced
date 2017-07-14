using Microsoft.AspNetCore.Mvc;
using Cibertec.UnitOfWork;
using Cibertec.Models;
using Microsoft.AspNetCore.Authorization;

namespace Cibertec.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("product")]
    [Authorize]
    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork unit) : base(unit)
        {
        }

        //private readonly IUnitOfWork _unit;
        //public CustomerController(IUnitOfWork unit)
        //{
        //    _unit = unit;
        //}

        [HttpGet]
        public IActionResult List()
        {
            return Ok(_unit.Products.GetAll());
        }

        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {
            return Ok(_unit.Products.Insert(product));
        }
    }
}
