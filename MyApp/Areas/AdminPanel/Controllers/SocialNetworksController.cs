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
    public class SocialNetworksController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: AdminPanel/SocialNetworks
        public ActionResult Index()
        {
            return View(db.SocialNetworks.ToList());
        }

        
       

        // GET: AdminPanel/SocialNetworks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminPanel/SocialNetworks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Img,IsShow,Link")] SocialNetwork socialNetwork)
        {
            if (ModelState.IsValid)
            {
                db.SocialNetworks.Add(socialNetwork);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(socialNetwork);
        }

        // GET: AdminPanel/SocialNetworks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialNetwork socialNetwork = db.SocialNetworks.Find(id);
            if (socialNetwork == null)
            {
                return HttpNotFound();
            }
            return View(socialNetwork);
        }

        // POST: AdminPanel/SocialNetworks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Img,IsShow,Link")] SocialNetwork socialNetwork)
        {
            if (ModelState.IsValid)
            {
                db.Entry(socialNetwork).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(socialNetwork);
        }

        // GET: AdminPanel/SocialNetworks/Delete/5
        public ActionResult Delete(int? id)
        {
            SocialNetwork socialNetwork = db.SocialNetworks.Find(id);
            db.SocialNetworks.Remove(socialNetwork);
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
