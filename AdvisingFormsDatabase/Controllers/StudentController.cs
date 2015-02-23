﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdvisingFormsDatabase.Models;
using AdvisingFormsDatabase.DAL;

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

            foreach (string prereq in course.Prerequisites)
            {
                if (!courseNames.Contains(prereq))
                {
                    prereqsMet = false;
                }
            }

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

            Concentration conc = db.Concentrations.Find(student.ConcentrationID);

            ViewBag.ConcentrationID = new SelectList(db.Concentrations, "ID", "Name", student.ConcentrationID);

            Student modelStudent=  MakeRecommendations(student);//var model = db.Concentrations
            //    .Where(r => r.ID == student.ConcentrationID);

            //List<BaseCourse> potentialCourses = student.StudentConcentration.RequiredCourses.ToList();


            return View(modelStudent);

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
