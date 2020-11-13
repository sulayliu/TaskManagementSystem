using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime CreatedTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public double Budget { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime FinishedTime { get; set; }//*
        public double TotalCost { get; set; }//*
        public ApplicationUser User { get; set; }
        public virtual ICollection<ProTask> ProTasks { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public Priority Priority { get; set; }        
        public Project()
        {
            Deadline = System.DateTime.Now;
            CreatedTime = System.DateTime.Now;
            ProTasks = new HashSet<ProTask>();
            Notifications = new HashSet<Notification>();
        }
    }
}