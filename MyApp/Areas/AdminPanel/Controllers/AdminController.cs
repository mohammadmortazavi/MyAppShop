using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyApp.Areas.AdminPanel.Controllers
{
    public class AdminController : Controller
    {
        // GET: AdminPanel/Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}