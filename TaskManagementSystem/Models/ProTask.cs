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
        public string TaskName { get; set; }

        [Display (Name ="Description")]
        public string TaskContent { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "Created Time")]
        public DateTime Time { get; set; }
        [Range(0, 100)]
        public double CompletedPercentage { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public string UserId { get; set; }
      
        public string UserName { get; set; }
        public ApplicationUser User { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public ProTask()
        {
            Time = System.DateTime.Now;
            Notes = new HashSet<Note>();
            Notifications = new HashSet<Notification>();
        }
    }
}