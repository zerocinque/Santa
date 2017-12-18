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

        public ActionResult Refresh()
        {
            return RedirectToAction("Index");
        }

        // GET: Orders/Details/5
        public ActionResult Details(string id)
        {
            if(string.IsNullOrEmpty(id))
                return RedirectToAction("Index");

            Order model = db.GetOrder(id);
            if(model == null)
            {
                ModelState.AddModelError("", "Order doesn't exists.");
                return RedirectToAction("Index");
            }

            for (int i = 0; i < model.Toys.Count; i++)
            {
                model.Toys[i] = db.GetToyByName(model.Toys[i].Name);
            }

            model.CountTotalAmount();

            return View(model);
        }

        public ActionResult PrepareAll()
        {
            var orders = db.GetAllOrders().ToList();
            var warehouse = db.GetAllToys().ToList();

            foreach (Order order in orders)
            {
                if (order.Status != OrderStatus.InProgress)
                    break;

                int[] indexToys = new int[order.Toys.Count];
                int i = 0;

                int flag = 0;
                foreach (Toy toy in order.Toys)
                {
                    foreach (Toy warehouseToy in warehouse)
                    {
                        if (warehouseToy.ID == toy.ID && warehouseToy.Amount < 0)
                        {
                            flag = -1;
                            break;
                        }

                        else if (warehouseToy.ID == toy.ID && warehouseToy.Amount > 0)
                        {
                            flag = 1;
                            indexToys[i++] = warehouse.GetHashCode();
                            break;
                        }
                    }

                    if (flag != 1)
                    {
                        flag = -1;
                        break;
                    }

                    flag = 0;
                }

                if (flag == -1)
                    continue;

                foreach (int j in indexToys)
                    warehouse[j].Amount--;

                order.Status = OrderStatus.ReadyToBeSend;
                if (!db.UpdateOrder(order))
                    ModelState.AddModelError("", string.Format("error uploding order with code {0}.", order.ID));
            }

            if (!db.UpdateAllToys(warehouse))
                ModelState.AddModelError("", "error uploding the warehouse.");

            return RedirectToAction("Index");
        }

        public ActionResult SendAll()
        {
            var orders = db.GetAllOrders().ToList();

            foreach (Order order in orders)
            {
                if (order.Status != OrderStatus.InProgress)
                    break;

                order.Status = OrderStatus.Done;
                if (!db.UpdateOrder(order))
                    ModelState.AddModelError("", string.Format("error uploding order with code {0}.", order.ID));
            }

            return RedirectToAction("Index");
        }

        public ActionResult send(string id)
        {
            try
            {
                uploadOrder(id, OrderStatus.Done);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Prepare(string id)
        {
            var warehouse = db.GetAllToys().ToList();
            Order order = db.GetOrder(id);

            if (order.Status != OrderStatus.InProgress)
            {
                ModelState.AddModelError("", "the order can't be prepare.");
                return RedirectToAction("Index");
            }

            int[] indexToys = new int[order.Toys.Count];
            int i = 0;

            int flag = 0;
            foreach (Toy toy in order.Toys)
            {
                foreach (Toy warehouseToy in warehouse)
                {
                    if (warehouseToy.ID == toy.ID && warehouseToy.Amount < 0)
                    {
                        flag = -1;
                        break;
                    }

                    else if (warehouseToy.ID == toy.ID && warehouseToy.Amount > 0)
                    {
                        flag = 1;
                        indexToys[i++] = warehouse.GetHashCode();
                        break;
                    }
                }

                if (flag != 1)
                {
                    flag = -1;
                    break;
                }

                flag = 0;
            }

            if (flag == -1)
            {
                ModelState.AddModelError("", "the order can't be prepare beacause there aren't enough toys.");
                return RedirectToAction("Index");
            }

            foreach (int j in indexToys)
                warehouse[j].Amount--;

            order.Status = OrderStatus.ReadyToBeSend;
            if (!db.UpdateOrder(order))
            {
                ModelState.AddModelError("", string.Format("error uploding order with code {0}.", order.ID));
                return RedirectToAction("Index");
            }

            if (!db.UpdateAllToys(warehouse))
                ModelState.AddModelError("", "error uploding the warehouse.");

            return RedirectToAction("Index");
        }

        private void uploadOrder(string id, OrderStatus status)
        {
            try
            {
                Order order = db.GetOrder(id);
                order.Status = status;
                if (!db.UpdateOrder(order))
                    ModelState.AddModelError("", "error uploding the order.");

                return;
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "There was some error.");
                throw ex;
            }
        }

    }
}
