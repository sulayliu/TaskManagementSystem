﻿using System;
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

        public static List<Project> GetProjectsWithTaskOrderByPercent()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var projects = db.Projects.Include("ProTasks").ToList();

            projects.ForEach(p =>
            {
                p.ProTasks = p.ProTasks.OrderByDescending(t => t.CompletedPercentage).ToList();
            });

            db.Dispose();
            return projects;
        }

        public static List<Project> GetProjectsWithTaskOrderByPriority()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var projects = db.Projects.Include("ProTasks").ToList();

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

        public static void Create(string UserId, string UserName, string Name, string Content, double Budget, DateTime Deadline)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            int Id = GetNewId();
            Project project = new Project
            {
                Id = Id,
                Name = Name,
                Content = Content,
                CreatedTime = DateTime.Now,
                IsCompleted = false,
                UserId = UserId,
                UserName = UserName,
                Budget = Budget,
                Deadline = Deadline,
                FinishedTime = DateTime.Now
            };
            db.Projects.Add(project);
            db.SaveChanges();
            db.Dispose();
        }

        public static void Edit(int Id, string Name, string Content, bool IsCompleted, double Budget, DateTime Deadline)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Project project = GetProject(Id);
            project.Name = Name;
            project.Content = Content;
            project.IsCompleted = IsCompleted;
            project.Budget = Budget;
            project.Deadline = Deadline;
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
    }
}