using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        //UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(new ApplicationDbContext()));
        //RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
        // GET: Admin
        public ActionResult Index()
        {
            //var users = UserManager.ShowAllUsers();
            return View();
        }

        public ActionResult ShowAllUsers()
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
            ViewBag.UserName = db.Users.Find(userId).UserName;
            ViewBag.UserId = userId;
            //var roles = userManager.GetRoles(userId).ToList();
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

        public ActionResult AddUserToRole(string userId)
        {
            ViewBag.UserName = db.Users.Find(userId).UserName;
            ViewBag.roleName = new SelectList(db.Roles, "Name","Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUserToRole(string roleName, string userId)
        {
            var r = UserManager.AddUserToRole(userId,roleName);
            if (r.Succeeded)
            {
                db.SaveChanges();
            }
            
            ViewBag.UserName = db.Users.Find(userId).UserName;
            ViewBag.roleName = new SelectList(db.Roles, "Name","Name");
            db.Dispose();
            return RedirectToAction("ShowAllRolesOfTheUser", new {userId});
        }
        public ActionResult DeleteRole(string roleName)
        {
            if (!UserManager.IsRoleExist(roleName))
            {
                return HttpNotFound();
            }
            ViewBag.RoleName = roleName;
            return View();
        }
        [HttpPost, ActionName("DeleteRole")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string roleName)
        {
            UserManager.DeleteRole(roleName);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("ShowAllRoles");
        }
    }
}