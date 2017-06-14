using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cibertec.Web.Models;
using Cibertec.UnitOfWork;

namespace Cibertec.Web.Controllers
{
    public class ProductController : Controller
    {
        //private readonly NorthwinddbContext _db;
        ////esta variable solo puede ser usada en el contructor y en ningun otro lado.

        //public ProductController(NorthwinddbContext db)
        //{
        //    _db = db;
        //}

      

        //public IActionResult Index()
        //{
        //    return View(_db.Products);
        //}

        private readonly IUnitOfWork _unit;
        //esta variable solo puede ser usada en el contructor y en ningun otro lado.

        public ProductController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public IActionResult Index()
        {
            return View(_unit.Products.GetAll());
        }
    }
}