using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;

namespace MyApp.Areas.AdminPanel.Controllers
{
    public class AdminController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: AdminPanel/Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SiteSetting()
        {
            var set1 = db.Settings.FirstOrDefault();
            return View(set1);
        }

        [HttpPost]
        public ActionResult SiteSetting( Setting set , HttpPostedFileBase file1)
        {
            var set2 = db.Settings.FirstOrDefault();

            file1.SaveAs(HttpContext.Server.MapPath("~/images/Logo/") +file1.FileName);
            set2.Logo = file1.FileName;
            set2.Name = set.Name;
            set2.Key = set.Key;
            set2.Des = set.Des;


            db.SaveChanges();

            return Redirect("/AdminPanel/Admin/Index");
        }

        public ActionResult CallSetting()
        {
            var set1 = db.Settings.FirstOrDefault();
            return View(set1);
        }

        [HttpPost]
        public ActionResult CallSetting(Setting set)
        {
            var set2 = db.Settings.FirstOrDefault();
            set2.Address = set.Address;
            set2.Tel = set.Tel;
            set2.Mobile = set.Mobile;
            set2.Fax = set.Fax;

            db.SaveChanges();
            return Redirect("/AdminPanel/Admin/Index");
        }

        public ActionResult SmsSetting()
        {
            var set1 = db.Settings.FirstOrDefault();
            return View(set1);
        }


        [HttpPost]
        public ActionResult SmsSetting(Setting set)
        {
            var set2 = db.Settings.FirstOrDefault();
            set2.UserSms = set.UserSms;
            set2.PassSms = set.PassSms;
            set2.SenderSms = set.SenderSms;
            set2.ActiveUser = set.ActiveUser;
            set2.SendUser = set.SendUser;


            db.SaveChanges();
            return Redirect("/AdminPanel/Admin/Index");
        }

        public ActionResult FactorSetting()
        {
            var set1 = db.Settings.FirstOrDefault();
            return View(set1);
        }

        [HttpPost]

        public ActionResult FactorSetting(Setting set)
        {
            var set2 = db.Settings.FirstOrDefault();
            set2.TaxPercent = set.TaxPercent;
            set2.ServicePric = set.ServicePric;
            set2.ServiceBetween = set.ServiceBetween;
            db.SaveChanges();
            return Redirect("/AdminPanel/Admin/Index");
        }


       
    }
}