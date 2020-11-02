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
        public string Content { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Time { get; set; }
        public bool IsCompleted { get; set; }
        public string ManagerId { get; set; }
        public virtual ApplicationUser ProjectManager { get; set; }
        public virtual ICollection<ProTask> ProTasks { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public Project()
        {
            Time = System.DateTime.Now;
            ProTasks = new HashSet<ProTask>();
            Notifications = new HashSet<Notification>();
        }
    }
}