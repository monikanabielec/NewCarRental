using NewCarRental.Helpers;
using NewCarRental.Models;
using NewCarRental.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewCarRental.Controllers
{
    public class LoginController : Controller
    {
        private CarRentalEntities db = new CarRentalEntities();
       
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Customers.Where(a => a.Login == model.Login && a.Password == model.Password).FirstOrDefault();
                if (user != null)
                {
                    UserHelper.User = user;
                }
                return RedirectToAction("Index", "Reservations");
            }
            return View("Index");
        }
    }
}