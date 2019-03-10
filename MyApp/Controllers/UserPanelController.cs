using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;
using System.Web.Security;

namespace MyApp.Controllers
{
    [Authorize]
    public class UserPanelController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: UserPanel
        public ActionResult Index()
        {
            var user = db.Users.FirstOrDefault(u => u.Mobile == User.Identity.Name);
            if (user.IsActive==false)
            {
                return RedirectToAction("Activate","Account");
            }
            else
            {
                return View();
            }
            
        }
        public ActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel change)
        {
            string hashpass = FormsAuthentication.HashPasswordForStoringInConfigFile(change.OldPassword,"MD5");
            var user = db.Users.FirstOrDefault(u => u.Mobile == User.Identity.Name && u.Password == hashpass);

            if (user!=null)
            {
                user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(change.Password, "MD5");
                db.SaveChanges();
                if (user.Roles.RoleName=="admin")
                {
                    return Redirect("/AdminPanel/Admin");
                }
                else
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");

            }
            else
            {
                ModelState.AddModelError("OldPassword", "کلمه عبور جاری اشتباه است");
            }
            return View(change);
        }
    }
}