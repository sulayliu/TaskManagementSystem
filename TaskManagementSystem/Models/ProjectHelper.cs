using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Linq;
using Microsoft.Ajax.Utilities;

namespace TaskManagementSystem.Models
{
    public static class ProjectHelper
    {
        public static List<Project> GetProjects()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var projects = db.Projects.Include("ProTasks").ToList();
            db.Dispose();
            return projects;
        }

        public static List<Project> GetProjectsByManager(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var projects = db.Projects.Where(p => p.UserId == userId).OrderByDescending(pr => pr.Priority).Include("ProTasks").ToList();
            db.Dispose();
            return projects;
        }

        public static List<Project> GetProjectsWithTaskOrderByPercent(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var projects = db.Projects.Where(p => p.UserId == userId).Include("ProTasks").ToList();

            projects.ForEach(p =>
            {
                p.ProTasks = p.ProTasks.OrderByDescending(t => t.CompletedPercentage).ToList();
            });

            db.Dispose();
            return projects;
        }

        public static List<Project> GetProjectsWithTaskOrderByPriority(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var projects = db.Projects.Where(p => p.UserId == userId).Include("ProTasks").ToList();

            projects.ForEach(p =>
            {
                p.ProTasks = p.ProTasks.OrderByDescending(t => t.Priority).ToList();
            });

            db.Dispose();
            return projects;
        }

        public static Project GetProject(int Id)
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

        public static int GetNewId()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            if (db.Projects.ToList().Count > 0)
            {
                Project project = db.Projects.ToList().Last();
                return project.Id + 1;
            }
            else
            {
                return 1;
            }
        }

        public static void Create(string UserId, string UserName, string Name, string Content, double Budget, DateTime Deadline, Priority Priority)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            int Id = GetNewId();
            Project project = new Project
            {
                Id = Id,
                Name = Name,
                Content = Content,
                Priority = Priority,
                CreatedTime = DateTime.Now,
                IsCompleted = false,
                UserId = UserId,
                UserName = UserName,
                Budget = Budget,
                Deadline = Deadline,
                FinishedTime = Deadline
            };
            db.Projects.Add(project);
            db.SaveChanges();
            db.Dispose();
        }

        public static void Edit(int Id, string Name, string Content, bool IsCompleted, double Budget, DateTime Deadline, Priority Priority)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Project project = GetProject(Id);
            project.Name = Name;
            project.Content = Content;
            project.IsCompleted = IsCompleted;
            project.Budget = Budget;
            project.Deadline = Deadline;
            project.Priority = Priority;
            db.Entry(project).State = EntityState.Modified;
            db.SaveChanges();
            db.Dispose();
        }
        public static void Edit(int Id, DateTime finishTime, bool IsCompleted, double totalCost)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Project project = GetProject(Id);
            project.IsCompleted = IsCompleted;
            project.FinishedTime = finishTime;
            project.TotalCost = totalCost;
            db.Entry(project).State = EntityState.Modified;
            db.SaveChanges();
            db.Dispose();
        }

        public static bool Delete(int Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Project project = db.Projects.Find(Id);

            foreach (Note note in db.Notes)
            {
                if (note.ProjectId == project.Id)
                {
                    db.Notes.Remove(note);
                }
            }

            if (project != null)
            {
                project.Notes.Clear();
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

        //method for projects that are exceeded budget
        public static List<ProTask> GetExceededDeadlines()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var tasks = db.Projects.SelectMany(p => p.ProTasks.Where(t => t.Deadline < System.DateTime.Now && p.IsCompleted == false)).ToList();

            return tasks;
        }

        //method for projects that are overdue
        public static List<Project> GetExceededBudgets()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var projects = db.Projects.Where(p => p.Budget < p.TotalCost).Include("ProTasks").ToList();
            db.Dispose();
            return projects;
        }

        // Check if the project are completed
        public static void SetProjectCompleted()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var today = DateTime.Now;
            List<Project> projects = db.Projects.Include(p => p.ProTasks).ToList();
            bool projectIsCompleted;
            foreach (var project in projects)
            {
                projectIsCompleted = true;
                foreach (var task in project.ProTasks)
                {
                    if (task.CompletedPercentage != 100) projectIsCompleted = false;
                }
                if (projectIsCompleted)
                {
                    Edit(project.Id, today, projectIsCompleted, CalculateCosts(project, today));
                }
            }

            db.Dispose();
        }

        //calculate the total cost of project after the project is completed
        public static double CalculateCosts(Project project, DateTime FinishedTime)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var users = db.Users.ToList();
            HashSet<ApplicationUser> usersOfTheProject = new HashSet<ApplicationUser>();
            foreach (var task in project.ProTasks)
            {
                usersOfTheProject.Add(db.Users.Find(task.UserId));

            }

            var projectManager = db.Users.Find(project.UserId);
            TimeSpan duration = FinishedTime.Subtract(project.CreatedTime);
            var dailyCost = Math.Round((usersOfTheProject.Sum(u => u.Salary) + projectManager.Salary), 2);
            var totalCost = Math.Round((duration.Days * dailyCost), 2);
            
            return totalCost;
        }
    }
}