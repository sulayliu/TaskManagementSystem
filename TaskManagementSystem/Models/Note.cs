using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Comment{ get; set; }

        [ForeignKey("Task")]
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }
        public string DeveloperId { get; set; }
        public virtual ApplicationUser Developer { get; set; }
    }
}