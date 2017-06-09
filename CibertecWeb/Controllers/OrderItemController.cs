using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cibertec.Web.Models;
using Cibertec.UnitOfWork;

namespace Cibertec.Web.Controllers
{
    public class OrderItemController : Controller
    {
        //private readonly NorthwinddbContext _db;
        ////esta variable solo puede ser usada en el contructor y en ningun otro lado.

        //public OrderItemController(NorthwinddbContext db)
        //{
        //    _db = db;
        //}

        //public IActionResult Index()
        //{
        //    return View(_db.OrderItems);
        //}

        private readonly IUnitOfWork _db;
        //esta variable solo puede ser usada en el contructor y en ningun otro lado.

        public OrderItemController(IUnitOfWork db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.OrderItems.GetAll());
        }

    }
}