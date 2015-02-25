using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdvisingFormsDatabase.DAL;
using AdvisingFormsDatabase.Models;

namespace AdvisingFormsDatabase.Controllers
{
    public class ConcentrationsController : Controller
    {
        private AdvisingFormsContext db = new AdvisingFormsContext();

        // GET: Concentrations
        public ActionResult Index()
        {
            return View(db.Concentrations.ToList());
        }

        // GET: Concentrations/Details/5
        public ActionResult Details(int? id)
        {

            var advisingStudent = db.Students.Find(id);
            Concentration concentration = db.Concentrations.Find(advisingStudent.ConcentrationID);
            var courseTaken = db.Courses
                .Where(r => r.StudentID == id);
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            

            if (concentration == null)
            {
                return HttpNotFound();
            }
           



            List<BaseCourse> untakenCouses = new List<BaseCourse>();
            foreach (BaseCourse baseCourse in concentration.RequiredCourses)
            {
                foreach (Course course in courseTaken)
                {

                    if (!baseCourse.ID.Equals(course.BaseCourseID))
                    {

                        untakenCouses.Add(baseCourse);
                        break;
                    }
                    else { break; }

                }


            }

            return View(untakenCouses);

        }
        // GET: Concentrations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Concentrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,HoursRequired")] Concentration concentration)
        {
            if (ModelState.IsValid)
            {
                db.Concentrations.Add(concentration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(concentration);
        }

        // GET: Concentrations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concentration concentration = db.Concentrations.Find(id);
            if (concentration == null)
            {
                return HttpNotFound();
            }
            return View(concentration);
        }

        // POST: Concentrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,HoursRequired")] Concentration concentration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(concentration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(concentration);
        }

        // GET: Concentrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concentration concentration = db.Concentrations.Find(id);
            if (concentration == null)
            {
                return HttpNotFound();
            }
            return View(concentration);
        }

        // POST: Concentrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Concentration concentration = db.Concentrations.Find(id);
            db.Concentrations.Remove(concentration);
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
