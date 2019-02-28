using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;
using System.Web.Security;
using MyApp.Classes;

namespace MyApp.Controllers
{
    public class AccountController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                string haspass = FormsAuthentication.HashPasswordForStoringInConfigFile(register.Password, "MD5");
                Random rand = new Random();
                int mycode = rand.Next(100000, 900000);


                if (!db.Users.Any(u => u.Mobile == register.Mobile))
                {
                    User user = new User()

                    {

                        Mobile = register.Mobile,
                        Password = haspass,
                        RoleId = db.Roles.Max(r => r.Id),
                        CodeNumber = mycode.ToString()
                };
                    db.Users.Add(user);
                    db.SaveChanges();

                    SmsSender sms = new SmsSender();
                    sms.Send(register.Mobile, "ثبت نام شما در فروشگاه انجام شد" + Environment.NewLine + "کد فعال سازی شما " + mycode.ToString());
                    return RedirectToAction("");
                }
                
            else
            {
                    ModelState.AddModelError("Mobile", "شما فبلا ثبت نام کردید");

            }
        }
            return View(register);
    }
}
}