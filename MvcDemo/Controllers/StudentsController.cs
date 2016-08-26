using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcDemo.Models;

namespace MvcDemo.Controllers
{
    public class StudentsController : Controller
    {
        private StudentDBContext db = new StudentDBContext();

        // GET: Students
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Gender = new SelectList(db.Genders, "ID", "Name");
            return View(db.Students.Include("Gender").Include("Manager").ToList());
        }

        // Post: search students
        [HttpPost]
        public ActionResult Index(string SearchString, string gender)
        {
            ViewBag.Gender = new SelectList(db.Genders, "ID", "Name");

            var lst = db.Students.Where(p => p.Name.Contains(SearchString)).ToList();

            if (!string.IsNullOrEmpty(gender))
                lst = lst.Where(p => p.GenderId.Value == Convert.ToInt32(gender)).ToList();

            return View(lst.ToList());
        }

        // GET: Students/Create
        public ActionResult Create(int? id, string mode)
        {
            //code to get Gender
            ViewBag.Gender = new SelectList(db.Genders, "ID", "Name");
            //ViewBag.Gender = db.Genders;

            //code to get manager 
            ViewBag.Manager = new SelectList(db.Students.Where(t => t.ID != id), "ID", "Name");

            //code to get courses 
            ViewBag.Courses = db.Courses.ToList();

            if (id != null && Convert.ToInt32(id.Value) > 0)
            {
                if (mode.ToLower() == "edit")
                {
                    ViewBag.mode = "edit";
                }
                else
                {
                    ViewBag.mode = "delete";
                }

                Student student = db.Students.Find(id);

                //get courses selected..
                List<Course> lst = new List<Course>();
                foreach (var item in ViewBag.Courses as List<Course>)
                {
                    Course obj = new Course();
                    obj.ID = item.ID;
                    obj.Name = item.Name;
                    obj.IsChecked = db.StudentCourses.Where(p => p.StudentID == id && p.CourseID == item.ID).Count() > 0 ? true : false;
                    lst.Add(obj);
                }
                if (lst.Count > 0)
                    ViewBag.Courses = lst;

                if (student == null)
                {
                    return HttpNotFound();
                }
                return View(student);
            }
            else
            {
                return View();
            }
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student, string ButtonClick)
        {
            if (student == null)
            {
                return HttpNotFound();
            }

            //code to get Gender
            ViewBag.Gender = new SelectList(db.Genders, "ID", "Name");

            //code to get manager 
            ViewBag.Manager = new SelectList(db.Students, "ID", "Name");

            //code to get courses 
            ViewBag.Courses = db.Courses.ToList();

            List<Course> lst = new List<Course>();
            foreach (var item in ViewBag.Courses as List<Course>)
            {
                Course obj = new Course();
                obj.ID = item.ID;
                obj.Name = item.Name;
                obj.IsChecked = db.StudentCourses.Where(p => p.StudentID == student.ID && p.CourseID == item.ID).Count() > 0 ? true : false;
                lst.Add(obj);
            }
            if (lst.Count > 0)
                ViewBag.Courses = lst;


            if (ButtonClick.ToLower() == "edit")
            {
                ViewBag.mode = "edit";
            }
            else if (ButtonClick.ToLower() == "delete")
            {
                ViewBag.mode = "delete";
            }

            if (Convert.ToInt32(student.ID) > 0)
            {
                Student obj = db.Students.Find(student.ID);

                if (ButtonClick.ToLower() == "edit")
                {
                    if (ModelState.IsValid)
                    {
                        obj.Name = student.Name;
                        obj.DOB = student.DOB;
                        obj.GenderId = student.GenderId;
                        if (student.ManagerID != null)
                            obj.ManagerID = student.ManagerID;
                        db.Entry(obj).State = EntityState.Modified;                        

                        //code to studentAddress     
                        StudentAddress objStudEdit = db.StudentAddresses.Where(p => p.StudentId == student.ID).FirstOrDefault();

                        if (objStudEdit != null)
                        {
                            objStudEdit.StudentId = student.ID;
                            objStudEdit.Address1 = student.StudentAddress.Address1;
                            objStudEdit.Country = student.StudentAddress.Country;
                            objStudEdit.State = student.StudentAddress.State;
                            objStudEdit.City = student.StudentAddress.City;
                            db.Entry(objStudEdit).State = EntityState.Modified;
                            obj.StudentAddress = objStudEdit;
                        }                        

                        if (student.SelectedCourses != null && student.SelectedCourses.Length > 0)
                        {
                            //delete and update records in studentCourse tbl..                        
                            db.StudentCourses.RemoveRange(db.StudentCourses.Where(x => x.StudentID == student.ID));

                            StudentCourse studentCourse = null;
                            foreach (int item in student.SelectedCourses)
                            {
                                studentCourse = new StudentCourse();
                                studentCourse.StudentID = obj.ID;
                                studentCourse.CourseID = item;
                                studentCourse.EnrollmentDate = DateTime.Now;
                                db.StudentCourses.Add(studentCourse);
                            }
                        }

                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(student);
                }
                else
                {
                    //remove courses..
                    db.StudentCourses.RemoveRange(db.StudentCourses.Where(x => x.StudentID == student.ID));

                    //remove Address..
                    StudentAddress objStudAdd = db.StudentAddresses.Where(p => p.StudentId == student.ID).FirstOrDefault();
                    if (objStudAdd != null)
                        db.StudentAddresses.Remove(objStudAdd);

                    //remove student..
                    db.Students.Remove(obj);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    Student obj = new Student();
                    obj.Name = student.Name;
                    obj.DOB = student.DOB;
                    obj.GenderId = student.GenderId;
                    if (student.ManagerID != null)
                        obj.ManagerID = student.ManagerID;

                    db.Students.Add(obj);
                    db.SaveChanges();

                    //code to add studentAddress..

                    StudentAddress objStuAdd = new StudentAddress();

                    objStuAdd.StudentId = obj.ID;
                    objStuAdd.Address1 = student.StudentAddress.Address1;
                    objStuAdd.Country = student.StudentAddress.Country;
                    objStuAdd.State = student.StudentAddress.State;
                    objStuAdd.City = student.StudentAddress.City;

                    db.StudentAddresses.Add(objStuAdd);
                    db.SaveChanges();

                    if (student.SelectedCourses != null && student.SelectedCourses.Length > 0)
                    {
                        StudentCourse studentCourse = null;
                        foreach (int item in student.SelectedCourses)
                        {
                            studentCourse = new StudentCourse();
                            studentCourse.StudentID = obj.ID;
                            studentCourse.CourseID = item;
                            studentCourse.EnrollmentDate = DateTime.Now;
                            db.StudentCourses.Add(studentCourse);
                        }
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(student);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
