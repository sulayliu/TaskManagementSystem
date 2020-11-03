using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class TaskHelper
    {
        static ApplicationDbContext db = new ApplicationDbContext();
        public void AddTask(string content)
        {
            ProTask proTask = new ProTask();
            proTask.TaskContent = content;
            db.ProTasks.Add(proTask);
            db.SaveChanges();
        }
        public void DeleteTask(int id)
        {
            var proTask = db.ProTasks.Find(id);
            db.ProTasks.Remove(proTask);
            db.SaveChanges();
        }
        public void UpdateTask(int id, string content)
        {
            var proTask = db.ProTasks.Find(id);
            proTask.TaskContent = content;
            db.SaveChanges();
        }
        public void AssignTask(int id, string DeveloperId)
        {
            var proTask = db.ProTasks.Find(id);
            proTask.DeveloperId = DeveloperId;
            db.SaveChanges();
        }
    }
}