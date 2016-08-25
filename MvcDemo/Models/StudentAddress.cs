using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcDemo.Models
{
    public class StudentAddress
    {
        //scalar properties..
        [Key, ForeignKey("Student")]
        public int StudentId { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }        
        public int Zipcode { get; set; }

        [ForeignKey("country")]
        public int CountryId { get; set; }        

        [ForeignKey("state")]
        public int StateId { get; set; }        

        [ForeignKey("city")]
        public int CityId { get; set; }        

        //navigational properties..
        public virtual Student Student { get; set; }
        public virtual Country country { get; set; }
        public virtual State state { get; set; }
        public virtual City city { get; set; }

    }
}