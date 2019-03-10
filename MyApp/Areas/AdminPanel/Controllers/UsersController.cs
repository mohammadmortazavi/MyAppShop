using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;
using System.Web.Security;

namespace MyApp.Areas.AdminPanel.Controllers
{
    public class UsersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: AdminPanel/Users
        public ActionResult Index(string strsearch)
        {
            var users = db.Users.Include(u => u.Roles).ToList();
            
            if (!String.IsNullOrEmpty(strsearch))
            {
                users = users.Where(u => u.Mobile.Contains(strsearch)).ToList();
            }
            return View(users.ToList());
        }

        public ActionResult ShowAddress(int? id)
        {
            var address = db.UserAddresses.Where(a => a.Id == id).ToList();
            return View(address);

        }


        // GET: AdminPanel/Users/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleTitel");
            return View();
        }

        // POST: AdminPanel/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RoleId,Mobile,Password,CodeNumber,IsActive")] User user)
        {
            if (ModelState.IsValid)
            {
                if (!db.Users.Any(u =>u.Mobile == user.Mobile))
                {
                    Random rand = new Random();
                    int mycode = rand.Next(100000, 900000);
                    Models.User usr = new User()
                    {
                        IsActive = user.IsActive,
                        RoleId = user.RoleId,
                        Mobile = user.Mobile,
                        Password = FormsAuthentication.HashPasswordForStoringInConfigFile(user.Password, "MD5"),
                        CodeNumber = mycode.ToString()
                    };
                    db.Users.Add(usr);
                    db.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("Mobile", "نمی  توانید با این شماره همراه ثبت کنید");
                }

                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleTitel", user.RoleId);
            return View(user);
        }


        // GET: AdminPanel/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleTitel", user.RoleId);
            return View(user);
        }

        // POST: AdminPanel/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RoleId,Mobile,Password,CodeNumber,IsActive")] User user)
        {
            if (ModelState.IsValid)
            {
                var usr = db.Users.FirstOrDefault(u => u.Id == user.Id);
                if (!db.Users.Any(u => u.Id != user.Id && u.Mobile == user.Mobile))
                {
                    usr.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(user.Password, "MD5");
                    usr.RoleId = user.RoleId;
                    usr.Mobile = user.Mobile;
                    usr.IsActive = user.IsActive;
                    db.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("Mobile", "شماره همراه تکرای می باشد");
                }


                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleTitel", user.RoleId);
            return View(user);
        }

        // GET: AdminPanel/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
