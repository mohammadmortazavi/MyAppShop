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
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                string hashpass = FormsAuthentication.HashPasswordForStoringInConfigFile(login.Password, "MD5");
                var user = db.Users.FirstOrDefault(u => u.Mobile == login.Mobile && u.Password == hashpass);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(login.Mobile, true);
                    if (user.IsActive)
                    {

                        if (user.Roles.RoleName == "admin")
                        {
                            return Redirect("/AdminPanel/Admin/Index");

                        }
                        else
                        {
                            return RedirectToAction("Index", "UserPanel");
                        }

                    }
                    else
                    {
                        return RedirectToAction("Index", "UserPanel");
                    }

                }
                else
                {
                    ModelState.AddModelError("Password", "مشخصات کاربری اشتباه وارد شده است");
                }
            }
            return View();

        }

        public ActionResult Activate()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Activate(ActiveViewModel active)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => u.Mobile == User.Identity.Name && u.CodeNumber == active.CodeNumber);
                if (user != null)
                {
                    Random rand = new Random();
                    int mycode = rand.Next(100000, 900000);
                    user.IsActive = true;
                    user.CodeNumber.ToString();
                    db.SaveChanges();
                    return RedirectToAction("Index", "UserPanel");
                }
                else
                {
                    ModelState.AddModelError("CodeNumber", "کد تایید شما نامتعبر می باشد");
                }
            }
            return View(active);
        }


        public ActionResult CheckMobile( )
        {

            return View();
        }

        [HttpPost]
        public ActionResult CheckMobile(CheckMobileViewModel check)
        {

            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => u.Mobile == check.Mobile);

                if (user !=null)
                {
                    SmsSender sms = new SmsSender();
                    sms.Send(check.Mobile, "کد تایید شما برای تغییر کلمه عبور " + user.CodeNumber + "می باشد");

                    return RedirectToAction("ForgetPassword");
                }
                else
                {
                    ModelState.AddModelError("Mobile", "شما هنوز ثبت نام نکردید");
                }

            }

            return View(check);
        }
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(ForgetPasswordViewModel forget)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => u.CodeNumber == forget.CodeNumber);

                if (user !=null)
                {
                    string hashpass = FormsAuthentication.HashPasswordForStoringInConfigFile(forget.Password ,"MD5");

                    Random rand = new Random();
                    string mycode = rand.Next(100000, 900000).ToString();
                    user.Password = hashpass;
                    user.CodeNumber = mycode.ToString();
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("CodeNumber", "کد تایید شما معتبر نمی باشد");
                }


            }
            return View(forget);
        }


    }
}