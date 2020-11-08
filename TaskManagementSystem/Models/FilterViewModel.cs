using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class FilterViewModel
    {
        public List<string> FilterOptions { get; set; }
        public FilterViewModel()
        {
            FilterOptions = new List<string>() { "Creation Date", "Completion Percentage", "Priority" };
        }
    }
}