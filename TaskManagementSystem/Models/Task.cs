using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class Task
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public string TaskContent { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Time { get; set; }
        [Range(0, 100)]
        public double CompletedPercentage { get; set; }
        public string DeveloperId { get; set; }
        public virtual ApplicationUser Developer { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public Task()
        {
            Time = System.DateTime.Now;
            Notes = new HashSet<Note>();
            Notifications = new HashSet<Notification>();
        }
    }
}