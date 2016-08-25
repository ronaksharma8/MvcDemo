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
            return View(db.Students.Include("Gender").ToList());            
        }

        // Post: search students
        [HttpPost]
        public ActionResult Index(string SearchString,int gender)
        {
            ViewBag.Gender = new SelectList(db.Genders, "ID", "Name");

            var lst = db.Students.Where(p => p.Name.Contains(SearchString) && p.GenderId == gender);
            return View(lst.ToList());
        }

        //// GET: Students/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Student student = db.Students.Find(id);
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(student);
        //}

        // GET: Students/Create
        public ActionResult Create(int? id, string mode)
        {
            //code to get Gender
            ViewBag.Gender = new SelectList(db.Genders, "ID", "Name");

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
        public ActionResult Create([Bind(Include = "ID,Name,DOB")] Student student, string gender, string ButtonClick)
        {
            if (student == null)
            {
                return HttpNotFound();
            }

            //code to get Gender
            ViewBag.Gender = new SelectList(db.Genders, "ID", "Name");


            if (Convert.ToInt32(student.ID) > 0)
            {
                Student obj = db.Students.Find(student.ID);

                if (ButtonClick.ToLower() == "edit")
                {
                    if (ModelState.IsValid)
                    {
                        obj.Name = student.Name;
                        obj.DOB = student.DOB;
                        obj.GenderId = Convert.ToInt32(gender);
                        db.Entry(obj).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(student);
                }
                else
                {                    
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
                    obj.GenderId = Convert.ToInt32(gender);                    

                    db.Students.Add(obj);
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
