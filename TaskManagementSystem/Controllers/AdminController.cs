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
        public ActionResult ShowAllRolesOfTheUser(string userId)
        {
            var roles = UserManager.ShowAllRolesForAUser(userId);
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
            UserManager.CreateRole(roleName);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("ShowAllRoles");
        }

        public ActionResult AddUserToRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUserToRole(string roleName, string userId)
        {
            UserManager.AddUserToRole(roleName, userId);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index");
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