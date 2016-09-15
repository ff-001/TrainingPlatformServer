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
    public class TrainingsController : Controller
    {
        private PerceiveContext db = new PerceiveContext();

        // GET: Trainings
        public async Task<ActionResult> Index()
        {
            List<Training> TrainingList = await db.Trainings.ToListAsync();
            foreach (var t in TrainingList)
            {
                if (t.User == null)
                {
                    t.User = await db.Users.FindAsync(t.UserId);
                }
            }
            db.SaveChanges();
            return View(TrainingList);
        }

        public async Task<ActionResult> Selected(long? id)
        {
            var trainings = db.Trainings.Where(p => p.UserId == id);
            return View(await trainings.ToListAsync());
        }

        // GET: Trainings/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training training = await db.Trainings.FindAsync(id);
            if (training == null)
            {
                return HttpNotFound();
            }
            return View(training);
        }

        // GET: Trainings/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "ID", "FullName");
            return View();
        }

        // POST: Trainings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,CreatDate,Type,FaultCount,FinishDate,IsFinished,UserId")] Training training)
        {
            if (ModelState.IsValid)
            {
                training.CreatDate = DateTime.UtcNow;
                training.PausePosition = "";
                training.CurrentTaskId = 0;
                db.Trainings.Add(training);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "ID", "FullName", training.UserId);
            return View(training);
        }

        // GET: Trainings/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training training = await db.Trainings.FindAsync(id);
            if (training == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "ID", "FullName", training.UserId);
            return View(training);
        }

        // POST: Trainings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,CreatDate,Type,PauseTime,PausePosition,CurrentTaskId,FaultCount,FinishDate,IsFinished,UserId")] Training training)
        {
            if (ModelState.IsValid)
            {
                db.Entry(training).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "ID", "FullName", training.UserId);
            return View(training);
        }

        // GET: Trainings/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training training = await db.Trainings.FindAsync(id);
            if (training == null)
            {
                return HttpNotFound();
            }
            return View(training);
        }

        // POST: Trainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Training training = await db.Trainings.FindAsync(id);
            db.Trainings.Remove(training);
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
