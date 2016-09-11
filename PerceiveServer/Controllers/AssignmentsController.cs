using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PerceiveServer.DAL;
using PerceiveServer.Models;

namespace PerceiveServer.Controllers
{
    public class AssignmentsController : Controller
    {
        private PerceiveContext db = new PerceiveContext();

        // GET: Assignments
        public async Task<ActionResult> Index()
        {
            var assignments = db.Assignments.Include(a => a.Task).Include(a => a.Training);
            return View(await assignments.ToListAsync());
        }

        // GET: Assignments/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = await db.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // GET: Assignments/Create
        public ActionResult Create()
        {
            ViewBag.TaskID = new SelectList(db.Tasks, "ID", "ID");
            ViewBag.TrainingID = new SelectList(db.Trainings, "ID", "ID");
            return View();
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AssignmentID,TrainingID,TaskID,Date")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                db.Assignments.Add(assignment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TaskID = new SelectList(db.Tasks, "ID", "ID", assignment.TaskID);
            ViewBag.TrainingID = new SelectList(db.Trainings, "ID", "ID", assignment.TrainingID);
            return View(assignment);
        }

        // GET: Assignments/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = await db.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.TaskID = new SelectList(db.Tasks, "ID", "ID", assignment.TaskID);
            ViewBag.TrainingID = new SelectList(db.Trainings, "ID", "ID", assignment.TrainingID);
            return View(assignment);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AssignmentID,TrainingID,TaskID,Date")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TaskID = new SelectList(db.Tasks, "ID", "ID", assignment.TaskID);
            ViewBag.TrainingID = new SelectList(db.Trainings, "ID", "ID", assignment.TrainingID);
            return View(assignment);
        }

        // GET: Assignments/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = await db.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Assignment assignment = await db.Assignments.FindAsync(id);
            db.Assignments.Remove(assignment);
            await db.SaveChangesAsync();
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
