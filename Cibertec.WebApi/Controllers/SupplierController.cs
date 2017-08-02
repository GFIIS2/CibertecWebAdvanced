using Cibertec.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace Cibertec.WebApi.Controllers
{
    [Route("supplier")]
    public class SupplierController : BaseController
    {
        public SupplierController(IUnitOfWork unit) : base(unit)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_unit.Suppliers.GetAll());
        }
    }
}
