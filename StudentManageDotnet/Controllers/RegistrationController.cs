using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentManageDotnet.Models;

namespace StudentManageDotnet.Controllers
{
    public class RegistrationController : Controller
    {
        private StudentManagedotnetEntities db = new StudentManagedotnetEntities();

        // GET: Registration
        public ActionResult Index(string search,string searchCourse,string searchBatch)
        {
            var registrations = db.Registrations.Include(r => r.Batch).Include(r => r.Course);

            if (!String.IsNullOrEmpty(search)) 
            {
                return View(db.Registrations.Where(x => x.firstname.StartsWith(search) || search == null).ToList());


            }
            else if (!String.IsNullOrEmpty(searchCourse)) 
            {
                return View(db.Registrations.Where(x => x.Course.course1.StartsWith(searchCourse) || searchCourse == null).ToList());

            }
            else 
            {
                return View(db.Registrations.Where(x => x.Batch.batch1.StartsWith(searchBatch) || searchBatch == null).ToList());

            }
            

        }


        // GET: Registration/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // GET: Registration/Create
        public ActionResult Create()
        {
            ViewBag.batch_id = new SelectList(db.Batches, "id", "batch1");
            ViewBag.course_id = new SelectList(db.Courses, "id", "course1");
            return View();
        }

        // POST: Registration/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,firstname,lastname,nic,course_id,batch_id,telno")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Registrations.Add(registration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.batch_id = new SelectList(db.Batches, "id", "batch1", registration.batch_id);
            ViewBag.course_id = new SelectList(db.Courses, "id", "course1", registration.course_id);
            return View(registration);
        }

        // GET: Registration/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            ViewBag.batch_id = new SelectList(db.Batches, "id", "batch1", registration.batch_id);
            ViewBag.course_id = new SelectList(db.Courses, "id", "course1", registration.course_id);
            return View(registration);
        }

        // POST: Registration/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,firstname,lastname,nic,course_id,batch_id,telno")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.batch_id = new SelectList(db.Batches, "id", "batch1", registration.batch_id);
            ViewBag.course_id = new SelectList(db.Courses, "id", "course1", registration.course_id);
            return View(registration);
        }

        // GET: Registration/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // POST: Registration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Registration registration = db.Registrations.Find(id);
            db.Registrations.Remove(registration);
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
