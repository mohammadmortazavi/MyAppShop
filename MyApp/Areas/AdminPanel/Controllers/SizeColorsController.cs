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
    public class SizeColorsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: AdminPanel/SizeColors
        public ActionResult Index(int? id)
        {
            var sizeColors = db.SizeColors.Where(p=>p.ProductId==id).Include(s => s.Colors).Include(s => s.Prodcuts).Include(s => s.Sizes);
            ViewBag.myid = id;
            return View(sizeColors.ToList());
        }

        

        // GET: AdminPanel/SizeColors/Create
        public ActionResult Create(int ? id)
        {
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "NameColor");
            ViewBag.ProductId = new SelectList(db.Products.Where(p=>p.Id==id).ToList(), "Id", "Name");
            ViewBag.SizeId = new SelectList(db.Sizes, "Id", "NameSize");
            return PartialView();
        }

        // POST: AdminPanel/SizeColors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SizeId,ColorId,ProductId")] SizeColor sizeColor ,int? id)
        {
            if (ModelState.IsValid)
            {
                db.SizeColors.Add(sizeColor);
                db.SaveChanges();
                return Redirect("/AdminPanel/SizeColors/Index/" + id);
            }

            ViewBag.ColorId = new SelectList(db.Colors, "Id", "NameColor", sizeColor.ColorId);
            ViewBag.ProductId = new SelectList(db.Products.Where(p => p.Id == id).ToList(), "Id", "Name", sizeColor.ProductId);
            ViewBag.SizeId = new SelectList(db.Sizes, "Id", "NameSize", sizeColor.SizeId);
            return PartialView(sizeColor);
        }

        // GET: AdminPanel/SizeColors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeColor sizeColor = db.SizeColors.Find(id);
            if (sizeColor == null)
            {
                return HttpNotFound();
            }
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "NameColor", sizeColor.ColorId);
            ViewBag.ProductId = new SelectList(db.Products.Where(p=>p.Id==sizeColor.ProductId), "Id", "Name", sizeColor.ProductId);
            ViewBag.SizeId = new SelectList(db.Sizes, "Id", "NameSize", sizeColor.SizeId);
            return View(sizeColor);
        }

        // POST: AdminPanel/SizeColors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SizeId,ColorId,ProductId")] SizeColor sizeColor,int? id)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sizeColor).State = EntityState.Modified;
                db.SaveChanges();
                var sizecolor = db.SizeColors.Find(id);
                return Redirect("/adminPanel/SizeColors/Index/"+ sizeColor.ProductId);
            }
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "NameColor", sizeColor.ColorId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", sizeColor.ProductId);
            ViewBag.SizeId = new SelectList(db.Sizes, "Id", "NameSize", sizeColor.SizeId);
            return PartialView(sizeColor);
        }

        // GET: AdminPanel/SizeColors/Delete/5
        public ActionResult Delete(int? id )
        {
            SizeColor sizeColor = db.SizeColors.Find(id);
            db.SizeColors.Remove(sizeColor);
            db.SaveChanges();
            return Redirect("/AdminPanel/SizeColors/Index/" + sizeColor.ProductId);
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
