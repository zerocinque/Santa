using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Santa.Classes;

namespace Santa.Tests
{
    public class MockControllerTest : Controller
    {
        private IDataBase db;

        public MockControllerTest(IDataBase dbParam)
        {
            db = dbParam;
        }

        public ActionResult OrderEdit(Order order)
        {
            db.UpdateOrder(order);
            return View();
        }

    }
}
