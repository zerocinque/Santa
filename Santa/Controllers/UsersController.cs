using Santa.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Santa.Controllers
{
    public class UsersController : Controller
    {
        private IDataBase db;

        public UsersController(IDataBase dbParam)
        {
            db = dbParam;
        }

        // GET: Users
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            //aggiungere controlli
            if(string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password)){
                ModelState.AddModelError("", "Invalid UserName or Password");
                return View();
            }

            user.Password = Encrypt(user.Password);

            User dbUser = db.GetUser(user);

            if(dbUser != null)
            {
                Session["UserName"] = dbUser.Email;
                Session["ScreenName"] = dbUser.ScreenName;
                Session["admin"] = dbUser.IsAdmin;
                return RedirectToAction($"../Home/Index");
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName or Password");
                return View();
            }
        }
        public ActionResult Logout()
        {
            if (Session["UserName"] != null)
            {
                Session.Clear();
                return RedirectToAction("Logout");
            }
            return View();
        }

        private string Encrypt(string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);
            byte[] resultHash;
            SHA512 shaM = new SHA512Managed();
            resultHash = shaM.ComputeHash(data);
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < resultHash.Length; i++)
            {
                result.Append(resultHash[i].ToString("X2").ToLower());
            }
            return result.ToString();
        }

    }
}