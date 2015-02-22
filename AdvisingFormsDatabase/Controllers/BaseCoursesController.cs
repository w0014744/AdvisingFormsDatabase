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
    public class BaseCoursesController : Controller
    {
        private AdvisingFormsContext db = new AdvisingFormsContext();

        // GET: BaseCourses
        public ActionResult Index()
        {
            return View(db.BaseCourses.ToList());
        }

        // GET: BaseCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaseCourse baseCourse = db.BaseCourses.Find(id);
            if (baseCourse == null)
            {
                return HttpNotFound();
            }
            return View(baseCourse);
        }

        // GET: BaseCourses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BaseCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Department,CourseNumber,CreditHours")] BaseCourse baseCourse)
        {
            if (ModelState.IsValid)
            {
                db.BaseCourses.Add(baseCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(baseCourse);
        }

        // GET: BaseCourses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaseCourse baseCourse = db.BaseCourses.Find(id);
            if (baseCourse == null)
            {
                return HttpNotFound();
            }
            return View(baseCourse);
        }

        // POST: BaseCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Department,CourseNumber,CreditHours")] BaseCourse baseCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(baseCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(baseCourse);
        }

        // GET: BaseCourses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaseCourse baseCourse = db.BaseCourses.Find(id);
            if (baseCourse == null)
            {
                return HttpNotFound();
            }
            return View(baseCourse);
        }

        // POST: BaseCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BaseCourse baseCourse = db.BaseCourses.Find(id);
            db.BaseCourses.Remove(baseCourse);
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
