using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Management;

namespace TaskManagementSystem.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int? ProTaskId { get; set; }
        public virtual ProTask ProTask { get; set; }
        public bool Priority { get; set; }
        public string Comment { get; set; }
        public bool IsOpened { get; set; }
        public NotificationType NotificationType { get; set; }
    }
    public enum NotificationType
    {
        Normal,
        Urgent,
        Overdue,
        NextToExpire,
        Completed,
    }
}