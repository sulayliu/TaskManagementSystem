using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.EnterpriseServices;
using Microsoft.Ajax.Utilities;
using System.Data.Entity;

namespace TaskManagementSystem.Models
{
    public static class TaskHelper
    {
        public static List<ProTask> GetTasks()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var tasks = db.ProTasks.ToList();
            db.Dispose();
            return tasks;
        }
        public static ProTask GetTask(int Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ProTask proTask = db.ProTasks.Find(Id);
            db.Dispose();
            if (proTask == null)
            {
                return null;
            }
            return proTask;
        }
        public static void Create(int projectId, string Name, string Content, string userId, DateTime deadline, Priority priority, string Comment)
        {
            ApplicationDbContext db = new ApplicationDbContext();
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

        public static void Delete(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
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
        public static void Edit(int id, string taskName, string taskContent, string userId, DateTime deadline, Priority priority, double completedPercentage)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ProTask proTask = GetTask(id);
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
                db.Entry(proTask).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }
        public static void Assign(int id, string DeveloperId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ProTask proTask = GetTask(id);
            if (proTask != null)
            {
                proTask.UserId = DeveloperId;
                db.SaveChanges();
                db.Dispose();
            }
        }
        public static List<ProTask> GetDeveloperTasks(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List<ProTask> developerTasks = db.ProTasks.Where(t => t.UserId == userId).ToList();
            return developerTasks;
        }

        public static void EditDeveloperTask(int id, double completedPercentage)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var proTask = GetTask(id);
            if (proTask != null)
            {
                proTask.CompletedPercentage = completedPercentage;
                db.Entry(proTask).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }
        public static void EditComment(int id, string Comment)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var proTask = GetTask(id);
            if (proTask != null)
            {
                proTask.Comment = Comment;
                db.SaveChanges();
                db.Dispose();
            }
        }
        public static List<ApplicationUser> GetListOfUsers()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var users = db.Users.Where(u => u.Id != "02561e7e-ba49-46c1-adb3-b9a6b6130af9").ToList();
            return users;
        }
    }
}