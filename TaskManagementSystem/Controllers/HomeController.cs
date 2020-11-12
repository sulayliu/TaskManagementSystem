using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        // if (User.Identity.IsAuthenticated) { }
        //var name = User.Identity.Name;
        //var newUser = db.Users.FirstOrDefault(u => u.UserName == name);
        // [Authorize(Roles ="ProjectManger")]
        //[Authorize(Roles ="Developer")]
        //[Authorize(Roles ="ProjectManager","Developer")]
        public ActionResult Index()
        {
            ViewBag.Notification = NotificationHelper.GetNotificationCount(User.Identity.GetUserId());
            ViewBag.NotificationToManager = NotificationHelper.GetNotificationCountToManager(User.Identity.GetUserId());

            NotificationHelper.SetNotificationsByType();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}