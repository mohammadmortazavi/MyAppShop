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
    public class ProductsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: AdminPanel/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Brands).Include(p => p.GenderGategories).Include(p => p.ProductCategories);
            return View(products.ToList());
        }

       

        // GET: AdminPanel/Products/Create
        public ActionResult Create()
        {
            ViewBag.BrnadId = new SelectList(db.Brands, "Id", "Name");
            ViewBag.GenderCatId = new SelectList(db.GenderGategories, "Id", "Name");
            ViewBag.ProductCatId = new SelectList(db.ProductCategories, "Id", "Name");
            return View();
        }

        // POST: AdminPanel/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GenderCatId,ProductCatId,BrnadId,Name,Img,Des,SalePrice,NumberSeen,NumberSale")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrnadId = new SelectList(db.Brands, "Id", "Name", product.BrnadId);
            ViewBag.GenderCatId = new SelectList(db.GenderGategories, "Id", "Name", product.GenderCatId);
            ViewBag.ProductCatId = new SelectList(db.ProductCategories, "Id", "Name", product.ProductCatId);
            return View(product);
        }

        // GET: AdminPanel/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrnadId = new SelectList(db.Brands, "Id", "Name", product.BrnadId);
            ViewBag.GenderCatId = new SelectList(db.GenderGategories, "Id", "Name", product.GenderCatId);
            ViewBag.ProductCatId = new SelectList(db.ProductCategories, "Id", "Name", product.ProductCatId);
            return View(product);
        }

        // POST: AdminPanel/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GenderCatId,ProductCatId,BrnadId,Name,Img,Des,SalePrice,NumberSeen,NumberSale")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrnadId = new SelectList(db.Brands, "Id", "Name", product.BrnadId);
            ViewBag.GenderCatId = new SelectList(db.GenderGategories, "Id", "Name", product.GenderCatId);
            ViewBag.ProductCatId = new SelectList(db.ProductCategories, "Id", "Name", product.ProductCatId);
            return View(product);
        }

        // GET: AdminPanel/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
