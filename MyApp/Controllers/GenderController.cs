using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;

namespace MyApp.Controllers
{
    public class GenderController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: Gender
        public ActionResult Index(int? Id)
        {
            ViewBag.CatId = Id;
            var gender = db.GenderGategories.Find(Id);
            ViewBag.myimg = gender.Img;
            return View();
        }
    }
}