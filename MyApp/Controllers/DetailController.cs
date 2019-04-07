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
            var product = db.Products.Find(id);
            product.NumberSeen = product.NumberSeen + 1;
            db.SaveChanges();
            ViewBag.proId = id;
            ViewBag.price = product.SalePrice.ToString("n0");
            ViewBag.descrip = product.Des.ToString();
            return View();
        }
        public  ActionResult Gallery(int? id)
        {
            var product = db.Products.Find(id);
            ViewBag.proname = product.Name;
            var gallery = db.Galleries.Where(g => g.ProductId == id).ToList();
            return PartialView(gallery);

        }

        public  ActionResult Size(int? id)
        {
            var size = db.SizeColors.Where(s => s.ProductId == id);
            size = size.GroupBy(s => s.SizeId).Select(s => s.FirstOrDefault());
            return PartialView(size.ToList());
        }
        public ActionResult Color(int ? id)
        {
            var color = db.SizeColors.Where(s => s.ProductId == id);
            color = color.GroupBy(s => s.ColorId).Select(c => c.FirstOrDefault());
            return PartialView(color.ToList());
        }
        public ActionResult Pishnahadi(int ? id)
        {
            var pro = db.Products.Find(id);
            var pishnahad = db.Products.Where(p => p.Id != id &&
            p.ProductCategories.IsShow== true &&
            p.ProductCatId==pro.ProductCatId &&
            p.GenderCatId==pro.GenderCatId).OrderBy(p=>Guid.NewGuid()).Take(3);
            return PartialView(pishnahad.ToList());
        }

    }
}