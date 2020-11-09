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
        public string UserId { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int ProTaskId { get; set; }
        public bool Priority { get; set; }
        public string Comment { get; set; }
    }

}