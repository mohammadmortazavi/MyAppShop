using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;

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
    }
}