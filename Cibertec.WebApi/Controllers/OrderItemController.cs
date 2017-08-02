using Cibertec.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace Cibertec.WebApi.Controllers
{
    [Route("orderitem")]
    public class OrderItemController : BaseController
    {
        public OrderItemController(IUnitOfWork unit) : base(unit)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_unit.OrderItems.GetAll());
        }
    }
}
