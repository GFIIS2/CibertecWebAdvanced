using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cibertec.Web.Models;
using Cibertec.UnitOfWork;
using Cibertec.Web.Filter;

namespace Cibertec.Web.Controllers
{
    [ExceptionLoggerFilter]
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _unit;
        //private readonly NorthwinddbContext _db;


        //esta variable solo puede ser usada en el contructor y en ningun otro lado.

        public CustomerController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public IActionResult Index()
        {
            return View(_unit.Customers.GetAll());
        }
        public IActionResult Detail()
        {
            _unit.Customers.SearchByNames("Maria","Anders");
            return View();
        }
    }
}