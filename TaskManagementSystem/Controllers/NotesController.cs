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
    public class NotesController : Controller
    {
        //[Authorize(Roles = "Developer")]
        public ActionResult Index()
        {
            return View(NotificationHelper.GetNotificationOfUser(User.Identity.GetUserId()));
        }
        //[Authorize(Roles = "ProjectManger")]
        public ActionResult IndexManager()
        {
            return View(NotificationHelper.GetNotificationOfManager(User.Identity.GetUserId()));
        }

        //[Authorize(Roles = "ProjectManager, Developer")]
        // GET: NotesDetails/OpenNote/5
        public ActionResult OpenNote(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var note = NotificationHelper.GetNoteDetails((int)id);
            note.IsOpened = true;
            NotificationHelper.Edit(note.Id, User.Identity.GetUserId(), note.ProjectId, note.ProTaskId, note.Priority, note.Comment, note.IsOpened);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        //[Authorize(Roles = "Developer")]
        // GET: Notes/Create
        public ActionResult Create(int ProjectId, int ProTaskId)
        {
            ViewBag.ProjectId = ProjectId;
            ViewBag.ProTaskId = ProTaskId;
            return View();
        }

        //[Authorize(Roles = "Developer")]
        // POST: Notes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,ProjectId,ProTaskId,Priority,Comment")] Note note)
        {
            note.Priority = true;
            if (ModelState.IsValid)
            {
                NotificationHelper.Create(User.Identity.GetUserId(), note.ProjectId, note.ProTaskId, note.Priority, NotificationType.Urgent, note.Comment);
                return RedirectToAction("Index", "ProTasks", new { userId = User.Identity.GetUserId() });
            }

            return View(note);
        }
        //[Authorize(Roles = "ProjectManager, Developer")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = NotificationHelper.GetNoteDetails((int)id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }
        //[Authorize(Roles = "ProjectManager, Developer")]
        // POST: Notes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ProjectId,ProTaskId,Priority,Comment,IsOpened")] Note note)
        {
            if (ModelState.IsValid)
            {
                NotificationHelper.Edit(note.Id, User.Identity.GetUserId(), note.ProjectId, note.ProTaskId, note.Priority, note.Comment, note.IsOpened);

                return RedirectToAction("Index");
            }
            return View(note);
        }

        //[Authorize(Roles = "ProjectManager, Developer")]
        // GET: Notes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = NotificationHelper.GetNoteDetails((int)id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }
        //[Authorize(Roles = "ProjectManager, Developer")]
        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Note note = NotificationHelper.GetNoteDetails((int)id);
            NotificationHelper.Delete(note.Id);
            return RedirectToAction("Index", "Notes", new { userId = User.Identity.GetUserId() });
        }
    }
}
