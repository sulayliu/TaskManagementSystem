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
        public ActionResult Index()
        {
            var proTasks = db.ProTasks.Include(p => p.Project).Include(p => p.User);
            return View(proTasks.ToList());
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
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: ProTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int projectId, string taskName, string taskContent, string userId)

        {
            if (ModelState.IsValid)
            {
                taskHelper.CreateTask(projectId, taskName, taskContent, userId);
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
        public ActionResult Edit([Bind(Include = "Id,TaskName,TaskContent,UserId")] ProTask proTask)
        {
            if (ModelState.IsValid)
            {

                taskHelper.Edit(proTask.Id, proTask.TaskName, proTask.TaskContent, proTask.UserId);
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
    }
}
