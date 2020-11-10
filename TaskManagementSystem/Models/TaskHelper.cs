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
        public List<Note>GetAllNotification(ApplicationUser user)
        {
            var notification = db.Notes.Where(n => n.User.Id == user.Id).ToList();
            return notification;
        }
        public void SetNotificationToPassDeadLine(string userId)
        {
            bool wasExecuted = false;
            Queue<ProTask> notifedTask = new Queue<ProTask>();
            var date = DateTime.Now.AddDays(1);
            var taskForNotify = db.ProTasks
                .Where(p => DateTime.Compare( p.Deadline, date) <= 0 && DateTime.Compare(p.Deadline, DateTime.Now) >= 0 && p.UserId == userId)
                .ToList();

            taskForNotify.ForEach(task =>
            {
                if (!task.IsItOverdue)
                {
                    task.IsItOverdue = true;
                    notifedTask.Enqueue(task);
                }
            });

            while(notifedTask.Count>0 && !wasExecuted)
            {
                var res = notifedTask.Dequeue();
                projectHelper.CreateNote(res.UserId, res.ProjectId, res.Id, true, "This task has only one day left");
            }
            db.SaveChanges();
            db.Dispose();
        }
    }
}