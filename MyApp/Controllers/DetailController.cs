using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;

namespace MyApp.Controllers
{
    public class DetailController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: Detail
        public ActionResult Index(int? id)
        {
            ViewBag.proId = id;
            return View();
        }
        public  ActionResult Gallery(int? id)
        {
            var product = db.Products.Find(id);
            ViewBag.proname = product.Name;
            var gallery = db.Galleries.Where(g => g.ProductId == id).ToList();
            return PartialView(gallery);

        }
    }
}