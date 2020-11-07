using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class ProTasksController : Controller
    {
        private ApplicationDbContext db;
        private TaskHelper taskHelper;
        public ProTasksController()
        {
            db = new ApplicationDbContext();
            taskHelper = new TaskHelper();
        }

        // GET: ProTasks
        //public ActionResult Index()
        //{
        //    var proTasks = db.ProTasks.Include(p => p.Project).Include(p => p.User);
        //    return View(proTasks.ToList());
        //}

        public ActionResult Index(string userId)
        {
            var developerProTasks = taskHelper.GetDeveloperTasks(userId);
            //var proTasks = db.ProTasks.Include(p => p.Project).Include(p => p.User);
            return View(developerProTasks);
        }


        // GET: ProTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProTask proTask = db.ProTasks.Find(id);
            if (proTask == null)
            {
                return HttpNotFound();
            }
            return View(proTask);
        }

        // GET: ProTasks/Create
        public ActionResult Create()
        {
            //ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: ProTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int projectId, string Name, string Content, string userId, DateTime deadline, Priority priority)

        {
            if (ModelState.IsValid)
            {
                taskHelper.CreateTask(projectId, Name, Content, userId,deadline,priority);
                return RedirectToAction("Index", "Projects");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");

            return View();
        }

        // GET: ProTasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProTask proTask = db.ProTasks.Find(id);
            if (proTask == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", proTask.UserId);
            return View(proTask);
        }

        // POST: ProTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,TaskContent,UserId,Deadline,Priority")] ProTask proTask)
        {
            if (ModelState.IsValid)
            {

                taskHelper.Edit(proTask.Id, proTask.Name, proTask.Content, proTask.UserId,proTask.Deadline,proTask.Priority);
                return RedirectToAction("Index","Projects");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // GET: ProTasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProTask proTask = db.ProTasks.Find(id);
            if (proTask == null)
            {
                return HttpNotFound();
            }
            return View(proTask);
        }

        // POST: ProTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProTask proTask = db.ProTasks.Find(id);
            db.ProTasks.Remove(proTask);
            db.SaveChanges();
            return RedirectToAction("Index","Projects");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: ProTasks/Edit for Developer
        public ActionResult EditDeveloperTask(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProTask proTask = db.ProTasks.Find(id);
            if (proTask == null)
            {
                return HttpNotFound();
            }
            //ViewBag.UserId = new SelectList(db.Users, "Id", "Email", proTask.UserId);
            return View(proTask);
        }

        // POST: ProTasks/Edit for Developer        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDeveloperTask([Bind(Include = "Id,CompletedPercentage")] ProTask proTask)
        {
            if (ModelState.IsValid)
            {
                taskHelper.EditDeveloperTask(proTask.Id, proTask.CompletedPercentage);                
                return RedirectToAction("Index", "ProTasks", new { userId = User.Identity.GetUserId() });
            }
            //ViewBag.UserId = new SelectList(db.Users, "Id", "Email");            
            return View();
        }

        // GET: ProTasks/Delete/5
        public ActionResult DeleteDeveloperTask(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProTask proTask = db.ProTasks.Find(id);
            if (proTask == null)
            {
                return HttpNotFound();
            }
            return View(proTask);
        }

        // POST: ProTasks/Delete/5
        [HttpPost, ActionName("DeleteDeveloperTask")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDeveloperTaskConfirmed(int id)
        {
            //ProTask proTask = db.ProTasks.Find(id);
            //db.ProTasks.Remove(proTask);
            taskHelper.Delete(id);
            db.SaveChanges();
            return RedirectToAction("Index", "ProTasks", new { userId = User.Identity.GetUserId() });
        }
    }
}
