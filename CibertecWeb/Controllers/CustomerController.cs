using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CibertecWeb.Models;

namespace CibertecWeb.Controllers
{
    public class CustomerController : Controller
    {
        private readonly NorthwinddbContext _db;
        //esta variable solo puede ser usada en el contructor y en ningun otro lado.

        public CustomerController(NorthwinddbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Customers);
        }
    }
}