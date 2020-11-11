using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.EnterpriseServices;
using Microsoft.Ajax.Utilities;

namespace TaskManagementSystem.Models
{
    public class TaskHelper
    {
        private ApplicationDbContext db;
        private ProjectHelper projectHelper;
        public TaskHelper()
        {
            db = new ApplicationDbContext();
            projectHelper = new ProjectHelper();
        }

        public List<ProTask> GetTasks()
        {
            var tasks = db.ProTasks.ToList();
            db.Dispose();
            return tasks;
        }

        public ProTask GetTask(int Id)
        {
            ProTask proTask = db.ProTasks.Find(Id);
            // db.Dispose();
            if (proTask == null)
            {
                return null;
            }
            return proTask;
        }

        public void CreateTask(int projectId, string Name, string Content, string userId, DateTime deadline, Priority priority, string Comment)
        {
            var user = db.Users.Find(userId);
            ProTask proTask = new ProTask
            {
                Name = Name,
                Content = Content,
                UserName = user.UserName,
                ProjectId = projectId,
                UserId = userId,
                CreatedTime = DateTime.Now,
                Deadline = deadline,
                Priority = priority,
                Comment = Comment
            };
            db.ProTasks.Add(proTask);
            db.SaveChanges();
            db.Dispose();
        }

        public void Delete(int id)
        {
            var proTask = db.ProTasks.Find(id);

            foreach (Note note in db.Notes)
            {
                if (note.ProTaskId == proTask.Id)
                {
                    db.Notes.Remove(note);
                }
            }

            if (proTask != null)
            {
                db.ProTasks.Remove(proTask);
                db.SaveChanges();
                db.Dispose();
            }
        }

        public void Edit(int id, string taskName, string taskContent, string userId, DateTime deadline, Priority priority, double completedPercentage)
        {
            var proTask = GetTask(id);
            var user = db.Users.Find(userId);
            if (proTask != null)
            {
                proTask.UserName = user.UserName;
                proTask.Name = taskName;
                proTask.Content = taskContent;
                proTask.UserId = userId;
                proTask.Deadline = deadline;
                proTask.Priority = priority;
                proTask.CompletedPercentage = completedPercentage;
                db.SaveChanges();
                db.Dispose();
            }
        }
        public void Assign(int id, string DeveloperId)
        {
            ProTask proTask = GetTask(id);
            if (proTask != null)
            {
                proTask.UserId = DeveloperId;
                db.SaveChanges();
                db.Dispose();
            }
        }

        //Developer side:
        public List<ProTask> GetDeveloperTasks(string userId)
        {
            List<ProTask> developerTasks = db.ProTasks.Where(t => t.UserId == userId).ToList();
            return developerTasks;
        }

        public void EditDeveloperTask(int id, double completedPercentage)
        {
            var proTask = GetTask(id);
            if (proTask != null)
            {
                proTask.CompletedPercentage = completedPercentage;
                db.SaveChanges();
                db.Dispose();
            }
        }
        public void EditComment(int id, string Comment)
        {
            var proTask = GetTask(id);
            if (proTask != null)
            {
                proTask.Comment = Comment;
                db.SaveChanges();
                db.Dispose();
            }
        }

        //P.Manager side:
        public List<ProTask> GetManagerProjectsAndTasks(string userId)
        {
            List<ProTask> managerProjectsAndTasks = db.ProTasks.Where(t => t.UserId == userId).ToList();
            return managerProjectsAndTasks;
        }
        //developer side
        //Notification of all user
        public List<Note> GetAllNotification(ApplicationUser user)
        {
            var notification = db.Notes.Where(n => n.User.Id == user.Id).ToList();
            return notification;
        }

        public string GetNotificationCount(string user)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var notification = db.Notes.Where(n => n.User.Id == user).ToList();
            db.Dispose();
            return notification.Count().ToString();
        }

        public string GetNotificationCountToManager(string userId)
        {
            ProjectHelper projectHelper = new ProjectHelper();

            return projectHelper.GetNotificationToManager(userId).Count().ToString();
        }

        //List of notification to be marked as read or opened
        public void MarkNotificationRead(List<Note> notes)
        {
            foreach (var notification in notes)
            {
                notification.IsOpened = true;
                db.SaveChanges();
                db.Dispose();
            }
        }
        //list of all unopened notification of the user
        public List<Note> GetNotificationUser(string userId)
        {
            var notification = db.Notes.Where(u => u.User.Id == userId && !u.IsOpened).ToList();
            return notification;
        }

        //Get all opened notification of the users
        public List<Note> GetAllNotification(string userId)
        {
            var notifications = db.Notes.Where(t => t.User.Id == userId).ToList();
            if (notifications.Count > 0)
            {
                MarkNotificationRead(notifications);
            }
            return notifications;
        }
    }
}