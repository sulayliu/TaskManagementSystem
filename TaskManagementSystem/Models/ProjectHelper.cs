using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Linq;
using Microsoft.Ajax.Utilities;

namespace TaskManagementSystem.Models
{
    public class ProjectHelper
    {
        public List<Project> GetProjects()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var projects = db.Projects.Include("ProTasks").ToList();
            db.Dispose();
            return projects;
        }

        public List<Project> GetProjectsWithTaskOrderByPercent()
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

        public List<Project> GetProjectsWithTaskOrderByPriority()
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

        public int GetNewId()
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

        public void Create(string UserId, string UserName, string Name, string Content)
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
                UserName = UserName
            };
            db.Projects.Add(project);
            db.SaveChanges();
            db.Dispose();
        }

        public void Edit(int Id, string Name, string Content, bool IsCompleted)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Project project = GetProject(Id);
            project.Name = Name;
            project.Content = Content;
            project.IsCompleted = IsCompleted;
            db.Entry(project).State = EntityState.Modified;
            db.SaveChanges();
            db.Dispose();
        }

        public bool Delete(int Id)
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

        //Notes Methods

        public List<Note> ListOfNotes()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var notes = db.Notes.Include(n => n.Project);
            return notes.ToList();
        }  
        public List<Note> NotificationOfUser(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var notes = db.Notes.Include(n => n.Project).Include(n =>n.ProTask).Where(n => n.UserId == userId);
            return notes.ToList();
        }

        public List<Note> GetNotificationToManager(string userId)
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
            return notes;
        }

        public Note GetNoteDetails(int Id)
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

        public void CreateNote(string UserId, int ProjectId, int ProTaskId, bool Priority, string Comment)
        {
            ApplicationDbContext db = new ApplicationDbContext();
             Note note = new Note
            {
            UserId=UserId,
            ProjectId=ProjectId,
            ProTaskId=ProTaskId,
            Priority=Priority,
            Comment=Comment
            };
            db.Notes.Add(note);
            db.SaveChanges();
            db.Dispose();
        }

        public void EditNote(int Id, string UserId, int ProjectId, int ProTaskId, bool Priority, string Comment)
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

        public bool DeleteNote(int Id)
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