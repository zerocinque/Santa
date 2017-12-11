using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Santa.Classes;

namespace Santa.Tests
{
    public class MockOrderControllerTest : Controller
    {
        private IDataBase db;

        public MockOrderControllerTest(IDataBase dbParam)
        {
            db = dbParam;
        }

        public ActionResult Edit(Order order)
        {
            db.UpdateOrder(order);
            return View();
        }

    }
}
