using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMPSAdvising.DAL;
using CMPSAdvising.Models;
using CMPSAdvising.ViewModels;

namespace CMPSAdvising.Controllers
{
    public class StudentsController : Controller
    {
        private CMPSAdvisingContext db = new CMPSAdvisingContext();

        public ActionResult AddCourseVM (int? id)
        {
            Student student = db.Students.Find(id);
            // List<BaseCourse> potentialCourses = student.StudentConcentration.RequiredCourses.ToList();
            List<BaseCourse> potentialCourses = db.BaseCourses.ToList();
            AddCourseViewModel vModel = new AddCourseViewModel(student, potentialCourses);

            List<Course> listCourses = new List<Course>();

            foreach(BaseCourse baseC in potentialCourses)
            {
                Course c = new Course();
                c.BaseCourse = baseC;
                c.Student = student;
                listCourses.Add(c);
            }

            vModel.PossibleCourses = listCourses;

            return View("AddCourseVM", vModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCourseVM (AddCourseViewModel vModel)
        {
            Student stu = db.Students.Find(vModel.Student.ID);
            foreach (Course c in vModel.PossibleCourses)
            {
                if (c.Selected)
                {
                    BaseCourse bc = db.BaseCourses.Find(c.BaseCourse.ID);
                    c.BaseCourse = bc;
                    c.Student = stu;
                    stu.CoursesTaken.Add(c);
                    db.Entry(c).State = EntityState.Added;
                }
            }

            if (stu != null)
            {
                db.Entry(stu).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("ListTakenCourses", stu);
        }

        public ActionResult ListTakenCourses (Student stu)
        {
            List<Course> taken = db.Courses.Where(c => c.StudentID == stu.ID).ToList();
            foreach (Course c in taken)
            {
                c.BaseCourse = db.BaseCourses.Find(c.BaseCourseID);
                c.Student = stu;
                stu.CoursesTaken.Add(c);
            }
            ViewBag.CoursesTaken = taken;

            return View(stu);
        }

        public ActionResult ClearCourses (int? id)
        {
            Student stu = db.Students.Find(id);
            int count = stu.CoursesTaken.Count;
            for (int i = 0; i < count; i++)
            {
                Course c = stu.CoursesTaken.First();
                stu.CoursesTaken.Remove(c);
            }
            db.Entry(stu).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        // GET: Students
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = db.Students.Find(id);
            List<Course> taken = student.CoursesTaken.ToList();
            foreach(Course c in taken)
            {
                c.BaseCourse = db.BaseCourses.Find(c.BaseCourse.ID);
            }
            ViewBag.CoursesTaken = taken;

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,WNumber,HoursCompleted,GPA")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,WNumber,HoursCompleted,GPA")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
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
