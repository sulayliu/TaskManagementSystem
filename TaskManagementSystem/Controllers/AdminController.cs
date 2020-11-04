using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            var users = UserManager.ShowAllUsers();
            return View(users);
        }
        public ActionResult ShowAllRoles()
        {
            var roles = UserManager.ShowAllRoles();
            return View(roles);
        }
        public ActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRole(string roleName)
        {

        }
        public ActionResult AddUserToRole()
        {
            ViewBag.UserId = 
        }
        public ActionResult DeleteRole(string roleName)
        {
            UserManager.DeleteRole(roleName);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("ShowAllRoles");
        }
    }
}