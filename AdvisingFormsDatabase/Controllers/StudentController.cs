using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdvisingFormsDatabase.Models;
using AdvisingFormsDatabase.DAL;
using AdvisingFormsDatabase.ViewModels;

namespace AdvisingFormsDatabase.Controllers
{
    public class StudentController : Controller
    {
        private AdvisingFormsContext db = new AdvisingFormsContext();

        public Student AddCourse (Student student, Course course)
        {
            student.CoursesTaken.Add(course);
            student.HoursCompleted = student.HoursCompleted + course.BaseCourse.CreditHours;
            return student;
        }

        public void RecommendCourse (Student student, BaseCourse course)
        {
            bool prereqsMet = true;
            List<string> courseNames = new List<string>();
            
            foreach (Course takenCourse in student.CoursesTaken)
            {
                courseNames.Add(takenCourse.BaseCourse.Name);
            }

            //foreach (string prereq in course.Prerequisites)
            //{
            //    if (!courseNames.Contains(prereq))
            //    {
            //        prereqsMet = false;
            //    }
            //}

            if (prereqsMet)
            {
                Course newCourse = new Course();
                newCourse.BaseCourse = course;
                newCourse.Student = student;
                student.CoursesRecommended.Add(newCourse);
            }
        }

        public Student MakeRecommendations (Student student)
        {
            
            
            List<BaseCourse> potentialCourses = student.StudentConcentration.RequiredCourses.ToList();

            foreach (Course takenCourse in student.CoursesTaken)
            {
                if (potentialCourses.Contains(takenCourse.BaseCourse))
                {
                    potentialCourses.Remove(takenCourse.BaseCourse);
                }
            }

            foreach (BaseCourse potentialCourse in potentialCourses)
            {
                RecommendCourse(student, potentialCourse);
            }

            return student;
        }

        public ActionResult Recommendation(int? id)
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
            ViewBag.ConcentrationID = new SelectList(db.Concentrations, "ID", "Name", student.ConcentrationID);

            List<BaseCourse> potentialCourses = student.StudentConcentration.RequiredCourses.ToList();

            return View(student);

        }



        public ActionResult AddCourses (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);

            List<BaseCourse> availibleCourses = db.BaseCourses.ToList();

            ViewData["AvailCourses"] = availibleCourses;

            return View(student);

        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AddCourses (Student student)
        //{

        //}



        public ActionResult AddCoursesVM (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = db.Students.Find(id);
            List<BaseCourse> availibleCourses = db.BaseCourses.ToList();

            AddCourseViewModel vModel = new AddCourseViewModel(student, availibleCourses);

            return View(vModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCoursesVM (AddCourseViewModel vModel)
        {
            if (ModelState.IsValid)
            {
                Student stu = vModel.Student;
                foreach (Course c in vModel.PossibleCourses)
                {
                    if (c.Selected)
                    {
                        stu.CoursesTaken.Add(c);
                    }
                }
                db.Entry(stu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListCoursesTaken", new { id = vModel.Student.ID });

            }

            return View(vModel);
        }

        public ActionResult ListCoursesTaken (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = db.Students.Find(id);

            return View(student);
        }



















        // GET: /Student/
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.StudentConcentration);

            return View(students.ToList());
        }

        // GET: /Student/Details/5
        public ActionResult Details(int? id)
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

        // GET: /Student/Create
        public ActionResult Create()
        {
            ViewBag.ConcentrationID = new SelectList(db.Concentrations, "ID", "Name");
            return View();
        }

        // POST: /Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,FirstName,LastName,WNumber,HoursCompleted,GPA,ConcentrationID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ConcentrationID = new SelectList(db.Concentrations, "ID", "Name", student.ConcentrationID);
            return View(student);
        }

        // GET: /Student/Edit/5
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
            ViewBag.ConcentrationID = new SelectList(db.Concentrations, "ID", "Name", student.ConcentrationID);
            return View(student);
        }

        // POST: /Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,FirstName,LastName,WNumber,HoursCompleted,GPA,ConcentrationID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConcentrationID = new SelectList(db.Concentrations, "ID", "Name", student.ConcentrationID);
            return View(student);
        }

        // GET: /Student/Delete/5
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

        // POST: /Student/Delete/5
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
