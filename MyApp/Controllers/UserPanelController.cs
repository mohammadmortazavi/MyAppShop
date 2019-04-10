using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;
using System.Web.Security;
using System.Globalization;
using MyApp.Classes;

namespace MyApp.Controllers
{
    [Authorize]
    public class UserPanelController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        PersianCalendar pc = new PersianCalendar();
        // GET: UserPanel
        public ActionResult Index()
        {
            var user = db.Users.FirstOrDefault(u => u.Mobile == User.Identity.Name);
            if (user.IsActive == false)
            {
                return RedirectToAction("Activate", "Account");
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
            string hashpass = FormsAuthentication.HashPasswordForStoringInConfigFile(change.OldPassword, "MD5");
            var user = db.Users.FirstOrDefault(u => u.Mobile == User.Identity.Name && u.Password == hashpass);

            if (user != null)
            {
                user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(change.Password, "MD5");
                db.SaveChanges();
                if (user.Roles.RoleName == "admin")
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

        public ActionResult Sale(int id)
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Sale(SaleViweModel sale, int id)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => u.Mobile == User.Identity.Name);
                if (user != null)
                {
                    string strtoday = pc.GetYear(DateTime.Now).ToString("0000") + "/" +
                        pc.GetMonth(DateTime.Now).ToString("00") + "/" +
                        pc.GetDayOfMonth(DateTime.Now).ToString("00");
                    var factor = db.Factors.FirstOrDefault(f => f.UserId == user.Id && f.IsPay == false);
                    if (factor != null)
                    {
                        var det = db.FactorDetails.FirstOrDefault(d => d.factorId == factor.Id && d.ProdcutId == id);
                        if (det != null)
                        {
                            det.Tedad = det.Tedad + sale.SaleCount;
                            db.SaveChanges();
                        }
                        else
                        {
                            FactorDetail newdetail = new FactorDetail()
                            {
                                factorId = factor.Id,
                                ProdcutId = id,
                                Tedad = sale.SaleCount
                            };
                            db.FactorDetails.Add(newdetail);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        Factor newfactor = new Factor()
                        {
                            UserId = user.Id,
                            IsPay = false,
                            DateFactor = strtoday,
                            CutPrice = 0,
                            NumberFactor = "",
                            SumItem = 0,
                            Tax = 0,
                            SumPrice = 0,
                            NumberControl = "",
                            DatePay = "",
                        };
                        db.Factors.Add(newfactor);
                        db.SaveChanges();
                        FactorDetail newdetail = new FactorDetail()
                        {
                            factorId = db.Factors.Max(f => f.Id),
                            ProdcutId = id,
                            Tedad = sale.SaleCount
                        };
                        db.FactorDetails.Add(newdetail);
                        db.SaveChanges();
                    }
                    return RedirectToAction("ShopingCart");
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }

            }
            return PartialView();
        }

        public ActionResult CountShopingCart()
        {
            var user = db.Users.FirstOrDefault(u => u.Mobile == User.Identity.Name);
            if (user != null)
            {
                var factor = db.Factors.FirstOrDefault(f => f.UserId == user.Id && f.IsPay == false);
                int detail = 0;
                if (factor != null)
                {
                    detail = db.FactorDetails.Count(d => d.factorId == factor.Id);

                }
                ViewBag.mycount = detail;
            }
            else
            {
                return RedirectToAction("Login", "Acccount");
            }
            return View();
        }
        public ActionResult ShopingCart()
        {
            var user = db.Users.FirstOrDefault(u => u.Mobile == User.Identity.Name);

            if (user != null)
            {
                var factor = db.Factors.FirstOrDefault(f => f.UserId == user.Id && f.IsPay == false);
                if (factor != null)
                {
                    var detail = db.FactorDetails.Where(d => d.factorId == factor.Id).ToList();
                    return View(detail);
                }
                else
                {
                    return Redirect("/");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult Delete(int id)
        {
            var detail = db.FactorDetails.Find(id);
            db.FactorDetails.Remove(detail);
            db.SaveChanges();
            return RedirectToAction("ShopingCart");
        }

        public ActionResult ShowFactor()
        {
            var user = db.Users.FirstOrDefault(u => u.Mobile == User.Identity.Name);
            if (user != null)
            {
                var setting = db.Settings.FirstOrDefault();
                ViewBag.taxpercent = setting.TaxPercent;
                ViewBag.sprice = setting.ServicePric;
                ViewBag.sbprice = setting.ServiceBetween;
                var factor = db.Factors.FirstOrDefault(f => f.UserId == user.Id && f.IsPay == false);
                var detail = db.FactorDetails.Where(d => d.factorId == factor.Id).ToList();
                return View(detail);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }



        }
        public ActionResult Factor()
        {
            var user = db.Users.FirstOrDefault(u => u.Mobile == User.Identity.Name);
            var factor = db.Factors.Where(f => f.IsPay == true && f.UserId == user.Id).OrderByDescending(f => f.DatePay).ToList();
            return View(factor);
        }
        public ActionResult FactorDetail( int ? id)
        {
            var user = db.Users.FirstOrDefault(u => u.Mobile == User.Identity.Name);
            var detail = db.FactorDetails.Where(f => f.factorId == id).ToList();
            return View(detail);
        }

    }
}