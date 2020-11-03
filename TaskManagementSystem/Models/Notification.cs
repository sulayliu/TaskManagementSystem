using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Details { get; set; }
        [ForeignKey("Project")]
        public int? ProjectId { get; set; }
        public virtual Project Project { get; set; }
        [ForeignKey("ProTask")]
        public int? ProTaskId { get; set; }
        public virtual ProTask ProTask { get; set; }
    }
}