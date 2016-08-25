using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MvcDemo.Models
{

    public class StudentDBContext : DbContext
    {

        public StudentDBContext() : base("name=StudentContext")
        {
            //Database.SetInitializer<StudentDBContext>(new StudentDBInitializer());
            Database.SetInitializer<StudentDBContext>(null);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentAddress> StudentAddresses { get; set; }

        //public DbSet<Country> Countries { get; set; }
        //public DbSet<State> States { get; set; }
        //public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //removes plural form from tbl name..
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //To rename column name in Bridge Tbl between Student and Course..
            modelBuilder.Entity<Student>()
                .HasMany<Course>(s => s.Courses)
                .WithMany(c => c.Students)
                .Map(cs =>
                {
                    cs.MapLeftKey("StudentId");
                    cs.MapRightKey("CourseId");
                    cs.ToTable("StudentCourse");
                });
        }
    }

    //code to have default Values in db tables...
    public class StudentDBInitializer : DropCreateDatabaseIfModelChanges<StudentDBContext>
    {
        protected override void Seed(StudentDBContext context)
        {
            //default Genders..
            IList<Gender> lstGender = new List<Gender>();

            lstGender.Add(new Gender() { ID = 1, Name = "Male" });
            lstGender.Add(new Gender() { ID = 2, Name = "Female" });            

            foreach (Gender gender in lstGender)
                context.Genders.Add(gender);

            //default courses..
            IList<Course> lstCourse = new List<Course>();

            lstCourse.Add(new Course() { ID = 1, Name = ".Net" });
            lstCourse.Add(new Course() { ID = 2, Name = "Java" });
            lstCourse.Add(new Course() { ID = 3, Name = "Php" });
            lstCourse.Add(new Course() { ID = 4, Name = "SEO" });
            lstCourse.Add(new Course() { ID = 5, Name = "WordPress" });
            
            foreach (Course course in lstCourse)
                context.Courses.Add(course);

            ////default countries..
            //IList<Country> lstCountry = new List<Country>();

            //Country country1 = new Country();
            //country1.ID = 1;
            //country1.Name = "Indai";

            //List<State> lstStates1 = new List<State>();
            //State state1 = new State();
            //state1.ID = 1;
            //state1.Name = "Gujarat";

            //lstStates1.Add(new State() { ID = 1, Name = "Gujarat", CountryId = 1 });
            //lstStates1.Add(new State() { ID = 2, Name = "Maharashtra", CountryId = 1 });            

            //country1.States = lstStates1;

            //Country country2 = new Country();
            //country2.ID = 1;
            //country2.Name = "Indai";
            //List<State> lstStates2 = new List<State>();
            //lstStates.Add(new State() { ID = 3, Name = "Florida", CountryId = 2 });
            //lstStates.Add(new State() { ID = 4, Name = "Michigan", CountryId = 2 });            
            //country1.States = lstStates2;

            //lstCountry.Add(new Country() { ID = 1, Name = "India" });
            //lstCountry.Add(new Country() { ID = 2, Name = "USA" });
            //lstCountry.Add(new Country() { ID = 3, Name = "China" });

            //foreach (Country country in lstCountry)
            //    context.Countries.Add(country);

            ////default states..
            //IList<State> lstStates = new List<State>();

            //lstStates.Add(new State() { ID = 1, Name = "Gujarat" , CountryId = 1 });
            //lstStates.Add(new State() { ID = 2, Name = "Maharashtra", CountryId = 1 });
            //lstStates.Add(new State() { ID = 3, Name = "Florida", CountryId = 2 });
            //lstStates.Add(new State() { ID = 4, Name = "Michigan", CountryId = 2 });
            //lstStates.Add(new State() { ID = 5, Name = "Province", CountryId = 3 });
            //lstStates.Add(new State() { ID = 6, Name = "Fuijan", CountryId = 3 });


            //foreach (State state in lstStates)
            //    context.States.Add(state);

            ////default courses..
            //IList<City> lstCity = new List<City>();

            //lstCity.Add(new City() { ID = 1, Name = "Baroda" , StateId = 1 });
            //lstCity.Add(new City() { ID = 2, Name = "Bombay" , StateId = 2 });
            //lstCity.Add(new City() { ID = 3, Name = "UsaCity_Florida" , StateId = 3 });
            //lstCity.Add(new City() { ID = 4, Name = "UsaCity_Michigan" , StateId = 4 });
            //lstCity.Add(new City() { ID = 5, Name = "ChinaCity_Province", StateId = 5 });
            //lstCity.Add(new City() { ID = 6, Name = "ChinaCity_Fuijan", StateId = 6 });


            //foreach (City city in lstCity)
            //    context.Cities.Add(city);

            base.Seed(context);
        }
    }
}