using Cibertec.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace Cibertec.WebApi.Controllers
{
    [Route("order")]
    public class OrderController : BaseController
    {
        public OrderController(IUnitOfWork unit) : base(unit)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_unit.Orders.GetAll());
        }
    }
}