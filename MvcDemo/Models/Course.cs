using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcDemo.Models
{
    public class Course
    {
        //scalar property
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        //navigation property
        public virtual ICollection<Student> Students { get; set; }

        //user defined
        [NotMapped]
        public bool IsChecked { get; set; }
    }
}