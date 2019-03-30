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
    public class BrandsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: AdminPanel/Brands
        public ActionResult Index()
        {
            return View(db.Brands.ToList());
        }

       
        

        // GET: AdminPanel/Brands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminPanel/Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Image")] Brand brand, HttpPostedFileBase file1)
        {
            if (ModelState.IsValid)
            {
                if (file1 != null)
                {
                    Random rand = new Random();
                    string imgcode = rand.Next(1000000, 9000000).ToString();
                    file1.SaveAs(HttpContext.Server.MapPath("~/images/Brand/") + imgcode.ToString() + "-" + file1.FileName);
                    brand.Img = imgcode.ToString() + "-" + file1.FileName;
                }
                db.Brands.Add(brand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(brand);
        }

        // GET: AdminPanel/Brands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: AdminPanel/Brands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Img")] Brand brand, HttpPostedFileBase file1)
        {
            if (ModelState.IsValid)
            {
                if (file1 != null)
                {
                    Random rand = new Random();
                    string imgcode = rand.Next(1000000, 9000000).ToString();
                    file1.SaveAs(HttpContext.Server.MapPath("~/images/Brand/") + imgcode.ToString() + "-" + file1.FileName);
                    brand.Img = imgcode.ToString() + "-" + file1.FileName;
                }
                db.Entry(brand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        // GET: AdminPanel/Brands/Delete/5
        public ActionResult Delete(int? id)
        {
            Brand brand = db.Brands.Find(id);
            db.Brands.Remove(brand);
            db.SaveChanges();
            return RedirectToAction("Index");
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
