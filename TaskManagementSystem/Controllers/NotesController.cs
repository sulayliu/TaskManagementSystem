﻿using Microsoft.AspNet.Identity;
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

        public ActionResult Index()
        {
            return View(NotificationHelper.GetNotificationOfUser(User.Identity.GetUserId()));
        }

        public ActionResult IndexManager()
        {
            return View(NotificationHelper.GetNotificationToManager(User.Identity.GetUserId()));
        }

        // GET: NotesDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var note = NotificationHelper.GetNoteDetails((int)id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // GET: Notes/Create
        public ActionResult Create(int ProjectId , int ProTaskId)
        {
            ViewBag.ProjectId = ProjectId;
            ViewBag.ProTaskId = ProTaskId;
            return View();
        }

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
                NotificationHelper.CreateNote(User.Identity.GetUserId(), note.ProjectId, note.ProTaskId, note.Priority, NotificationType.Urgent, note.Comment);
                return RedirectToAction("Index", "ProTasks", new { userId = User.Identity.GetUserId() });
            }
           
            return View(note);
        }

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
            //ViewBag.ProjectId = new SelectList(NotificationHelper.GetProjects(), "Id", "Name", note.ProjectId);
            return View(note);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ProjectId,ProTaskId,Priority,Comment")] Note note)
        {
            if (ModelState.IsValid)
            {
                NotificationHelper.EditNote(note.Id, User.Identity.GetUserId(), note.ProjectId, note.ProTaskId, note.Priority, note.Comment);

                return RedirectToAction("Index");
            }
            //ViewBag.ProjectId = new SelectList(NotificationHelper.GetProjects(), "Id", "Name", note.ProjectId);
            return View(note);
        }


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

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Note note = NotificationHelper.GetNoteDetails((int)id);
            NotificationHelper.DeleteNote(note.Id);
            return RedirectToAction("Index", "Notes", new { userId = User.Identity.GetUserId() });
        }
    }
}
