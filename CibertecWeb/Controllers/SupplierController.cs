using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cibertec.Web.Models;
using Cibertec.UnitOfWork;

namespace Cibertec.Web.Controllers
{
    public class SupplierController : Controller
    {
        //private readonly NorthwinddbContext _db;
        ////esta variable solo puede ser usada en el contructor y en ningun otro lado.

        //public SupplierController(NorthwinddbContext db)
        //{
        //    _db = db;
        //}

        //public IActionResult Index()
        //{
        //    return View(_db.Suppliers);
        //}

        private readonly IUnitOfWork _unit;
        //esta variable solo puede ser usada en el contructor y en ningun otro lado.

        public SupplierController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public IActionResult Index()
        {
            return View(_unit.Suppliers.GetAll());
        }
    }
}