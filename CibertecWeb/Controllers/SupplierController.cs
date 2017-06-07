using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CibertecWeb.Models;

namespace CibertecWeb.Controllers
{
    public class SupplierController : Controller
    {
        private readonly NorthwinddbContext _db;
        //esta variable solo puede ser usada en el contructor y en ningun otro lado.

        public SupplierController(NorthwinddbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Suppliers);
        }
    }
}