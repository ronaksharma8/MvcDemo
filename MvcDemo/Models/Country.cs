using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcDemo.Models
{
    public class Country
    {
        //scalar properties..
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        //navigational properties
        public virtual ICollection<State> States { get; set; }
    }

    public class State
    {
        //scalar properties..
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [ForeignKey("country")]
        public int CountryId { get; set; }

        //navigation properties
        public virtual Country country { get; set; }
        public virtual ICollection<City> Cities { get; set; }

    }

    public class City
    {
        //scalar properties..
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [ForeignKey("state")]
        public int StateId { get; set; }

        //navigation properties
        public virtual State state { get; set; }
    }
}