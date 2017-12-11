using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Santa.Classes;

namespace Santa.Controllers
{
    public class OrdersController : Controller
    {
        private IDataBase db;

        public OrdersController(IDataBase dbParam)
        {
            db = dbParam;
        }

    }
}