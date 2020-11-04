using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        
        // GET: Admin
        public ActionResult Index()
        {
            var users = UserManager.ShowAllUsers();
            return View(users);
        }
    }
}