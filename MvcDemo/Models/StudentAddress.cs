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

        [Required(ErrorMessage = "Address is required")]
        public string Address1 { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        //[ForeignKey("country")]
        //public int CountryId { get; set; }        

        //[ForeignKey("state")]
        //public int StateId { get; set; }        

        //[ForeignKey("city")]
        //public int CityId { get; set; }        

        //navigational properties..
        public virtual Student Student { get; set; }

        //public virtual Country country { get; set; }
        //public virtual State state { get; set; }
        //public virtual City city { get; set; }

    }
}