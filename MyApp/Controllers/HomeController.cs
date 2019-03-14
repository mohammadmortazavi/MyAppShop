using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;

namespace MyApp.Controllers
{
    public class HomeController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: Home
        public ActionResult Index()
        {
            DatabaseContext db = new DatabaseContext();
           
            return View(db.Roles.ToList());
        }
        public ActionResult Genders()
        {
            var gender = db.GenderGategories.ToList();
            return PartialView(gender);
        }
        public ActionResult Call()
        {
            var setting = db.Settings.FirstOrDefault();
            return PartialView(setting);
                
        }
    }
}