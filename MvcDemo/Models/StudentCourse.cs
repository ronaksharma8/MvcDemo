using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcDemo.Models
{
    public class StudentCourse
    {
        [Key, Column(Order = 0)]
        public int StudentID { get; set; }

        [Key, Column(Order = 1)]
        public int CourseID { get; set; }

        public virtual Student student { get; set; }
        public virtual Course course { get; set; }

        public DateTime? EnrollmentDate { get; set; }
        
    }
}