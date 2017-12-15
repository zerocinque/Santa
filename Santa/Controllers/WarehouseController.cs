using Santa.Classes;
using Santa.Models;
using System.Linq;
using System.Web.Mvc;

namespace Santa.Controllers
{
    [Authorize]
    public class WarehouseController : Controller
    {
        private IDataBase db;

        public WarehouseController(IDataBase dbParam)
        {
            db = dbParam;
        }

        // GET: Warehouse
        public ActionResult Index()
        {
            var warehouse = db.GetAllToys();
            Warehouse model = new Warehouse();
            model.EntityList = warehouse.ToList();
            return View(model);
        }

        public ActionResult Refresh()
        {
            return RedirectToAction("Index");
        }

    }
}
