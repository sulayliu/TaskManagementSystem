using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public static class NotificationHelper
    {
        public static void SetNotificationsByType()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var today = DateTime.Now;
            List<Project> projects = db.Projects.ToList();
            bool projectIsCompleted;
            string internalNoteKey = "";
            foreach (var project in projects)
            {
                projectIsCompleted = true;

                var NotesKey = db.Notes.Select(n => n.UserId + n.ProjectId + n.ProTaskId + n.Priority + n.NotificationType + n.Comment).ToList();

                foreach (var task in project.ProTasks)
                {
                    // Set Overdue Notification for task
                    if (task.CompletedPercentage < 100 && task.CompletedPercentage >= 0 && task.Deadline < DateTime.Now)
                    {
                        internalNoteKey = (project.UserId + project.Id + task.Id + true + NotificationType.Overdue + "The task is overdue").Trim();
                        if (!NotesKey.Contains(internalNoteKey))
                        {
                            CreateNote(project.UserId, project.Id, task.Id, true, NotificationType.Overdue, "The task is overdue");
                        }
                    }
                    // Set Completed Notification for task
                    if (task.CompletedPercentage == 100)
                    {
                        internalNoteKey = (project.UserId + project.Id + task.Id + true + NotificationType.Completed + "The task is completed").Trim();
                        if (!NotesKey.Contains(internalNoteKey))
                        {
                            CreateNote(project.UserId, project.Id, task.Id, true, NotificationType.Completed, "The task is completed");
                        }
                    }
                    else
                    {
                        projectIsCompleted = false;
                    }
                    // Set Overdue Notification for task
                    if ((task.Deadline - today).TotalHours <= 24 && (task.Deadline - today).TotalHours > 0 && !task.IsItOverdue)
                    {
                        task.IsItOverdue = true;
                        CreateNote(task.UserId, task.ProjectId, task.Id, true, NotificationType.NextToExpire, "This task has only one day left");
                    }
                }

                string notificationWithoutTask = "";
                internalNoteKey = (project.UserId + project.Id + notificationWithoutTask + true + NotificationType.Completed + "The project is completed").Trim();
                // Set Completed Notification for project
                if (!NotesKey.Contains(internalNoteKey))
                {
                    if (projectIsCompleted) CreateNote(project.UserId, project.Id, null, true, NotificationType.Completed, "The project is completed");
                }
            }
            db.Dispose();
        }
        public static List<Note> GetNotificationOfUser(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var notes = db.Notes.Include(p => p.Project).Include(t => t.ProTask).Where(n => n.UserId == userId);

            return notes.Where(n =>
                n.NotificationType == NotificationType.Normal ||
                n.NotificationType == NotificationType.NextToExpire ||
                n.NotificationType == NotificationType.Urgent
            ).ToList();
        }

        public static List<Note> GetNotificationToManager(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List<Note> notes = new List<Note>();
            var projects = db.Projects.Where(u => u.UserId == userId).ToList();
            foreach (var note in db.Notes)
            {
                foreach (var project in projects)
                {
                    if (note.ProjectId == project.Id)
                    {
                        notes.Add(note);
                    }
                }
            }

            return notes.Where(n =>
                n.NotificationType == NotificationType.Normal ||
                n.NotificationType == NotificationType.Completed ||
                n.NotificationType == NotificationType.Overdue ||
                n.NotificationType == NotificationType.Urgent
            ).ToList();
        }

        public static string GetNotificationCount(string user)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var notification = db.Notes.Where(n => n.User.Id == user).ToList();
            db.Dispose();
            return notification.Count().ToString();
        }

        public static string GetNotificationCountToManager(string userId)
        {
            return GetNotificationToManager(userId).Count().ToString();
        }

        //Notes Methods
        public static List<Note> ListOfNotes(string UserLoggedIn)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var notes = db.Notes.Include(n => n.Project).Where(n => n.UserId == UserLoggedIn);
            return notes.ToList();
        }

        public static Note GetNoteDetails(int Id)
        {
            {
                ApplicationDbContext db = new ApplicationDbContext();
                Note note = db.Notes.Find(Id);
                db.Dispose();
                if (note == null)
                {
                    return null;
                }
                return note;
            }
        }

        public static void CreateNote(string UserId, int ProjectId, int? ProTaskId, bool Priority, NotificationType NotificationType, string Comment)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            Note note = new Note
            {
                UserId = UserId,
                ProjectId = ProjectId,
                ProTaskId = ProTaskId,
                Priority = Priority,
                NotificationType = NotificationType,
                Comment = Comment
            };
            db.Notes.Add(note);
            db.SaveChanges();
            db.Dispose();
        }

        public static void EditNote(int Id, string UserId, int ProjectId, int? ProTaskId, bool Priority, string Comment)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Note note = GetNoteDetails(Id);
            note.Id = Id;
            note.UserId = UserId;
            note.ProjectId = ProjectId;
            note.ProTaskId = ProTaskId;
            note.Priority = Priority;
            note.Comment = Comment;
            db.Entry(note).State = EntityState.Modified;
            db.SaveChanges();
            db.Dispose();
        }
        public static bool DeleteNote(int Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Project project = db.Projects.Find(Id);
            if (project != null)
            {
                db.Projects.Remove(project);
                db.SaveChanges();
                db.Dispose();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}