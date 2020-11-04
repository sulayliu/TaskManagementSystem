using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class TaskHelper
    {
        static ApplicationDbContext db = new ApplicationDbContext();
        public List<ProTask> GetTasks()
        {
            var tasks = db.ProTasks.ToList();
            db.Dispose();
            return tasks;
        }

        public ProTask GetTask(int Id)
        {
            ProTask proTask = db.ProTasks.Find(Id);
            db.Dispose();
            if (proTask == null)
            {
                return null;
            }
            return proTask;
        }
        public void Create(string content)
        {
            ProTask proTask = new ProTask();
            proTask.TaskContent = content;
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
        public void Edit(int id, string content)
        {
            var proTask = GetTask(id);
            if(proTask != null)
            {
                proTask.TaskContent = content;
                db.SaveChanges();
                db.Dispose();
            }
        }
        public void Assign(int id, string DeveloperId)
        {
            ProTask proTask = GetTask(id);
            if(proTask != null)
            {
                proTask.UserId = DeveloperId;
                db.SaveChanges();
                db.Dispose();
            }
        }
    }
}