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

        [StringLength(50)]
        public string Name { get; set; }

        public DateTime DOB { get; set; }

        [ForeignKey("gender")]
        public int GenderId { get; set; }


        //navigation properties.

        //Foreign key for Standard       
        
        public virtual Gender gender { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
    
}