using Santa.Classes;
using Santa.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Santa.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IDataBase db;

        public HomeController(IDataBase dbParam)
        {
            db = dbParam;
        }


        public ActionResult Index()
        {
            Statistics model = new Statistics();
            List<Order> orders = (List<Order>)db.GetAllOrders();
            List<Toy> warehouse = (List<Toy>)db.GetAllToys();

            model.WarehouseToys = warehouse.Count;
            int finished = 0;
            foreach (Toy toy in warehouse)
                if (toy.Amount == 0)
                    finished++;

            model.FinishedToys = finished;

            foreach(Order order in orders)
            {
                switch (order.Status)
                {
                    case OrderStatus.Done:
                        model.OrderShipped++;
                        break;
                    case OrderStatus.InProgress:
                        model.OrdersRequest++;
                        break;
                    case OrderStatus.ReadyToBeSend:
                        model.OrdersReady++;
                        break;
                }
            }

            return View(model);
        }
    }
}