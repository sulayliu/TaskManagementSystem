using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class ProjectHelper
    {
        public List<Project> GetProjects()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var projects = db.Projects.ToList();
            db.Dispose();
            return projects;
        }

        public Project GetProject(int Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Project project = db.Projects.Find(Id);
            db.Dispose();
            if (project == null)
            {
                return null;
            }
            return project;
        }

        public void Create(int Id, string Content, bool IsCompleted, string ManagerId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (GetProject(Id) == null)
            {
                Project project = new Project
                {
                    Id = Id,
                    Content = Content,
                    Time = DateTime.Now,
                    IsCompleted = IsCompleted,
                    ManagerId = ManagerId
                };
                db.Projects.Add(project);
                db.SaveChanges();
                db.Dispose();
            }
        }

        public void Edit(int Id, string Content, bool IsCompleted, string ManagerId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Project project = GetProject(Id);
            project.Content = Content;
            project.IsCompleted = IsCompleted;
            project.ManagerId = ManagerId;
            db.Entry(project).State = EntityState.Modified;
            db.SaveChanges();
            db.Dispose();
        }

        public bool Delete(int Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Project project = db.Projects.Find(Id);
            if(project != null)
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