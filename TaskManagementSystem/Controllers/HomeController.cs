using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        // if (User.Identity.IsAuthenticated) { }
        //var name = User.Identity.Name;
        //var newUser = db.Users.FirstOrDefault(u => u.UserName == name);
        //[Authorize(Roles ="ProjectManger")]
        //[Authorize(Roles ="Developer")]
        //[Authorize(Roles = "ProjectManager, Developer")]
        public ActionResult Index()
        {
            ViewBag.Notification = NotificationHelper.CountUserNotifications(User.Identity.GetUserId());
            ViewBag.NotificationToManager = NotificationHelper.CountManagerNotifications(User.Identity.GetUserId());
            @ViewBag.Unread = NotificationHelper.CountUnopenedUserNotifications(User.Identity.GetUserId());
            @ViewBag.UnreadManager = NotificationHelper.CountUnopenedManagerNotifications(User.Identity.GetUserId());
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