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
        [Route("")]
        public IActionResult GetList()
        {
            return Ok(_unit.Products.GetAll());
        }

        [HttpGet]
        [Route("count")]
        public IActionResult GetCount()
        {
            return Ok(_unit.Products.RowNumbers());
        }

        [HttpGet]
        [Route("list/{numberPage}/{numberRow}")]
        public IActionResult GetListRange(int numberPage, int numberRow)
        {
            int startRow; int endRow;
            startRow = ((numberPage - 1) * numberRow) + 1;
            endRow = numberPage * numberRow;

            return Ok(_unit.Products.SearchByRange(startRow,endRow));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            return Ok(_unit.Products.GetById(id));
        }
                
        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {   
            var id = _unit.Products.Insert(product);
            return Ok(new
            {
                id = id
            });
        }
                
        [HttpPut]
        public IActionResult Put([FromBody]Product customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!_unit.Products.Update(customer)) return BadRequest("Incorrect id");
            return Ok(new { status = true });
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest();
            var result = _unit.Products.Delete(new Product { Id = id });
            return Ok(new { delete = true });
        }        
    }
}
