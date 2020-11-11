using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class ProgressHelper
    {
        // Class selector select the appropriate bootstrap class for progress bar based on input number.
        public static string ClassSelector(double number)
        {
            if (number <= 10)
            {
                return "progress-bar-danger";
            }
            else if (number <= 25)
            {
                return "progress-bar-warning";
            }
            else if (number <= 50)
            {
                return "progress-bar-info";
            }
            else if (number <= 75)
            {
                return "";
            }
            else if (number == 100)
            {
                return "progress-bar-success";
            }
            return "";
        }
    }
}