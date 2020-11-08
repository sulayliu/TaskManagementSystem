using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class ProTask
    {

        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Content { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime CreatedTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Deadline { get; set; }
        [Range(0, 100)]
        public double CompletedPercentage { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string UserName { get; set; }
        public Priority Priority { get; set; }
        public string Comment { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public ProTask()
        {
            Deadline = System.DateTime.Now;
            CreatedTime = System.DateTime.Now;
            Notes = new HashSet<Note>();
            Notifications = new HashSet<Notification>();
        }
    }

    public enum Priority
    {
        Low,
        Normal,
        High,
    }
}