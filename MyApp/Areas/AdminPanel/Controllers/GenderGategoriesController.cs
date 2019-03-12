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
    public class GenderGategoriesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: AdminPanel/GenderGategories
        public ActionResult Index()
        {
            return View(db.GenderGategories.ToList());
        }

        

        
        // GET: AdminPanel/GenderGategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenderGategory genderGategory = db.GenderGategories.Find(id);
            if (genderGategory == null)
            {
                return HttpNotFound();
            }
            return View(genderGategory);
        }

        // POST: AdminPanel/GenderGategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Img")] GenderGategory genderGategory , HttpPostedFileBase file1)
        {
            if (ModelState.IsValid)
            {
                if (file1 !=null)
                {
                    Random rand = new Random();
                    string imgcode = rand.Next(100000, 900000).ToString();
                    file1.SaveAs(HttpContext.Server.MapPath("~/images/Banner/") + imgcode.ToString() + "-" + file1.FileName);
                    genderGategory.Img = imgcode.ToString() + "-" + file1.FileName;
                }
                db.Entry(genderGategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genderGategory);
        }

        // GET: AdminPanel/GenderGategories/Delete/5
        public ActionResult Delete(int? id)
        {
            GenderGategory genderGategory = db.GenderGategories.Find(id);
            db.GenderGategories.Remove(genderGategory);
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
