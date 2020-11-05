using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class TaskHelper
    {
        //static ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationDbContext db;
        public TaskHelper()
        {
            db = new ApplicationDbContext();
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

        public void CreateTask(int projectId, string taskName, string taskContent, string userId)
        {
            var user = db.Users.Find(userId);
            ProTask proTask = new ProTask
            {
                TaskName = taskName,
                TaskContent = taskContent,
                UserName = user.UserName,
                ProjectId = projectId,
                UserId = userId,
                Time = DateTime.Now
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

        public void Edit(int id, string taskName, string taskContent, string userId)
        {
            var proTask = GetTask(id);
            var user = db.Users.Find(userId);
            if (proTask != null)
            {
                proTask.UserName = user.UserName;
                proTask.TaskName = taskName;
                proTask.TaskContent = taskContent;
                proTask.UserId = userId;
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
    }
}