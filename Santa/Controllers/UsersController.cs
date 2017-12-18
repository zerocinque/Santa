using Santa.Classes;
using System.Web.Mvc;
using Santa.Models;
using Santa.Infrastructure.Abstract;
using System.Web.Security;

namespace Santa.Controllers
{
    public class UsersController : Controller
    {
        IAuthProvider authProvider;
        private IDataBase db;

        public UsersController(IAuthProvider auth, IDataBase dbParam)
        {
            authProvider = auth;
            db = dbParam;
        }


        // GET: Users
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLogin user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User loggedUser = authProvider.Authenticate(user);
                if (loggedUser != null)
                {
                    Session["ScreenName"] = loggedUser.ScreenName;
                    Session["Admin"] = loggedUser.IsAdmin;
                    return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect email or password");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Incorrect email or password");
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}