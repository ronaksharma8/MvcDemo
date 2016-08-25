using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MvcDemo.Models
{
    public class Student
    {
        //scalar properties..
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "DOB is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime DOB { get; set; }

        [ForeignKey("gender")]
        [Required(ErrorMessage = "Gender is required")]
        public int? GenderId { get; set; }

        [ForeignKey("Manager")]        
        public int? ManagerID { get; set; }


        //navigation properties.               

        public virtual Gender gender { get; set; }
        public virtual Student Manager { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Student> SubOrdinates { get; set; }
    }
    
}