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
        private TaskHelper taskHelper;
        public HomeController()
        {
            taskHelper = new TaskHelper();
        }
        public ActionResult Index()
        {
            taskHelper.SetNotificationToPassDeadLine(User.Identity.GetUserId());
            ViewBag.Notification = taskHelper.GetNotificationCount(User.Identity.GetUserId());
            ViewBag.NotificationToManager = taskHelper.GetNotificationCountToManager(User.Identity.GetUserId());
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