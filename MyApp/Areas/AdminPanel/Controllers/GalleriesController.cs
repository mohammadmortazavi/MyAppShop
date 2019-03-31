using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;

namespace MyApp.Areas.AdminPanel.Controllers
{
    public class GalleriesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: AdminPanel/Galleries
        public ActionResult Index(int? Id)
        {
            var galleries = db.Galleries.Where(g=>g.ProductId==Id).Include(g => g.Products);
            ViewBag.myid = Id;
            return View(galleries.ToList());
        }

       

        // GET: AdminPanel/Galleries/Create
        public ActionResult Create(int? Id)
        {
            ViewBag.ProductId = new SelectList(db.Products.Where(g=>g.Id==Id).ToList(), "Id", "Name");
            return PartialView();
        }

        // POST: AdminPanel/Galleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,Img")] Gallery gallery,HttpPostedFileBase file1,int? Id)
        {
            
            if (ModelState.IsValid)
            {
                if (file1 != null)
                {
                    Random rand = new Random();
                    string imgcode = rand.Next(100000, 900000).ToString();
                    file1.SaveAs(HttpContext.Server.MapPath("~/images/Product/") + imgcode.ToString() + "-" + file1.FileName);
                    gallery.Img = imgcode.ToString() + "-" + file1.FileName;

                }
                db.Galleries.Add(gallery);
                db.SaveChanges();
                return Redirect("/AdminPanel/Galleries/Index/" + Id);
            }

            ViewBag.ProductId = new SelectList(db.Products.Where(g=>g.Id==Id).ToList(), "Id", "Name", gallery.ProductId);
            return PartialView(gallery);
        }

        // GET: AdminPanel/Galleries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products.Where(g => g.Id==gallery.ProductId).ToList(), "Id", "Name", gallery.ProductId);
            return PartialView(gallery);
        }

        // POST: AdminPanel/Galleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,Img")] Gallery gallery, HttpPostedFileBase file1 , int? Id)
        {
            if (ModelState.IsValid)
            {
                if (file1 != null)
                {
                    Random rand = new Random();
                    string imgcode = rand.Next(100000, 900000).ToString();
                    file1.SaveAs(HttpContext.Server.MapPath("~/images/Product/") + imgcode.ToString() + "-" + file1.FileName);
                    gallery.Img = imgcode.ToString() + "-" + file1.FileName;

                }
                db.Entry(gallery).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/AdminPanel/Galleries/Index/" + gallery.ProductId);
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", gallery.ProductId);
            return PartialView(gallery);
        }

        // GET: AdminPanel/Galleries/Delete/5
        public ActionResult Delete(int? id , int? gid)
        {
            Gallery gallery = db.Galleries.Find(id);
            db.Galleries.Remove(gallery);
            db.SaveChanges();
            return Redirect("/AdminPanel/Galleries/Index/" + gid);
        }

    

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
