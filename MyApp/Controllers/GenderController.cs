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

        public ActionResult Filter(int? Id)
        {
            var group = (from p in db.Products
                         join pcat in db.ProductCategories
                         on p.ProductCatId equals pcat.Id
                         where p.GenderCatId == Id
                         &&
                         pcat.IsShow == true
                         select pcat);
            group = group.GroupBy(p => p.Name).Select(p => p.FirstOrDefault());

            return PartialView(group.ToList());
        }

        public ActionResult ShowProduct(int? Id)
        {
            var show = db.Products.Where(p => p.GenderCatId == Id).ToList();
            return PartialView(show);
        }
        public ActionResult ShowSocial()
        {
            var social = db.SocialNetworks.Where(s => s.IsShow == true).ToList();
            return PartialView(social);
        }
        public ActionResult Search ( string strsearch)
        {
            var product = db.Products.Where(p => p.ProductCategories.IsShow == true &&
              (p.Name.Contains(strsearch) || p.GenderGategories.Name.Contains(strsearch) || p.ProductCategories.Name.Contains(strsearch) ||
              p.Brands.Name.Contains(strsearch) ||
              p.Des.Contains(strsearch))).OrderByDescending(p => p.Id).ToList();
            return View(product);
        }
    }
}