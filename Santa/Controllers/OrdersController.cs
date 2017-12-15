using Santa.Classes;
using Santa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Santa.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private IDataBase db;

        public OrdersController(IDataBase dbParam)
        {
            db = dbParam;
        }

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.GetAllOrders();
            Orders model = new Orders();
            model.EntityList = orders.ToList();
            return View(model);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Orders/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
