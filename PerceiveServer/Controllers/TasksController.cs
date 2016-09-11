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
    public class TasksController : Controller
    {
        private PerceiveContext db = new PerceiveContext();
        static long ParentId = 0;
        // GET: Tasks
        public async Task<ActionResult> Index()
        {
            return View(await db.Tasks.ToListAsync());
        }

        public async Task<ActionResult> Selected(long? id)
        {
            if (id == null)
            {
                id = ParentId;
            }
            ParentId = (long)id;
            Training training = await db.Trainings.FindAsync(id);
            List<Models.Task> tasks = new List<Models.Task>();
            foreach (var assignment in training.Assignment)
            {
                tasks.Add(assignment.Task);
            }
            return View(tasks);
        }

        // GET: Tasks
        public async Task<ActionResult> Add(long id)
        {
            Assignment assignment = new Assignment { TaskID = id, TrainingID = ParentId, Date = DateTime.UtcNow };
            Training training = await db.Trainings.FindAsync(ParentId);
            if (!training.Assignment.Contains(assignment))
            {
                db.Assignments.Add(assignment);
            }
            else
            {
                return RedirectToAction("AddMore");
            }
            await db.SaveChangesAsync();
            return RedirectToAction("Selected", new { id = ParentId });
        }
        // GET: Tasks/AddMore
        public async Task<ActionResult> AddMore()
        {
            return View(await db.Tasks.ToListAsync());
        }
        // GET: Tasks/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Task task = await db.Tasks.FindAsync(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,SourceID,DestinationID,SelectLevel,Instruction,SelectTransmitType,FaultCount")] Models.Task task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(task);
        }

        // GET: Tasks/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Task task = await db.Tasks.FindAsync(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,SourceID,DestinationID,SelectLevel,Instruction,SelectTransmitType,FaultCount")] Models.Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Task task = await db.Tasks.FindAsync(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<ActionResult> DeleteInTraining(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var assignment = db.Assignments.Where(p => p.TrainingID == ParentId).First(a => a.TaskID == id);
            db.Assignments.Remove(assignment);
            await db.SaveChangesAsync();
            return RedirectToAction("Selected", new { id = ParentId });
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Models.Task task = await db.Tasks.FindAsync(id);
            db.Tasks.Remove(task);
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
