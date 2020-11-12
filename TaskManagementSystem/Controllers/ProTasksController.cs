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
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(string userId)
        {
            var developerProTasks = TaskHelper.GetDeveloperTasks(userId);
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
        public ActionResult Create(int ProjectId, string Name, string Content, string UserId, DateTime Deadline, Priority Priority, string Comment)

        {
            if (ModelState.IsValid)
            {
                TaskHelper.CreateTask(ProjectId, Name, Content, UserId, Deadline, Priority, Comment);
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
        public ActionResult Edit([Bind(Include = "Id,Name,TaskContent,UserId,Deadline,Priority,CompletedPercentage")] ProTask proTask)
        {
            if (ModelState.IsValid)
            {

                TaskHelper.Edit(proTask.Id, proTask.Name, proTask.Content, proTask.UserId, proTask.Deadline, proTask.Priority, proTask.CompletedPercentage);
                return RedirectToAction("Index", "Projects");
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
            TaskHelper.Delete(id);
            db.SaveChanges();
            return RedirectToAction("Index", "Projects");
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
                TaskHelper.EditDeveloperTask(proTask.Id, proTask.CompletedPercentage);
                return RedirectToAction("Index", "ProTasks", new { userId = User.Identity.GetUserId() });
            }
            //ViewBag.UserId = new SelectList(db.Users, "Id", "Email");            
            return View();
        }
        //Get/protask/Comment for developers
        public ActionResult Comments(int? id)
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

        //Comments for developer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comments([Bind(Include = "Id, Comment")] ProTask proTask)
        {
            if (ModelState.IsValid)
            {
                TaskHelper.EditComment(proTask.Id, proTask.Comment);
                return RedirectToAction("Index", "Projects", new { userId = User.Identity.GetUserId() });
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
            TaskHelper.Delete(id);
            db.SaveChanges();
            return RedirectToAction("Index", "ProTasks", new { userId = User.Identity.GetUserId() });
        }
    }
}
