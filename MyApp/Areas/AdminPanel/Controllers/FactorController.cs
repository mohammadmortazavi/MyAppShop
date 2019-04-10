using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;

namespace MyApp.Areas.AdminPanel.Controllers
{
    
    public class FactorController : Controller
    {
        DatabaseContext db = new DatabaseContext();
       
        public ActionResult ShowFactor()
        {
            var factor = db.Factors.Where(f => f.IsPay == true).OrderByDescending(f => f.DatePay).ToList();
            return View(factor);
        }

        public ActionResult FactorDetail(int ? id)
        {
            var detail = db.FactorDetails.Where(d => d.factorId == id).ToList();
            return View(detail);
        }
    }
}