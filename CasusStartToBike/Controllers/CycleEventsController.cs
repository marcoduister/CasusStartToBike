using CasusStartToBike.Data;
using CasusStartToBike.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CasusStartToBike.Controllers
{
    public class CycleEventsController : Controller
    {
        private STBDContext db = new STBDContext();

        // GET: CycleEvents
        public ActionResult Index()
        {
            var cycleEvent = db.CycleEvent.Include(c => c.Badge).Include(c => c.CycleRoute).Include(c => c.User);
            return View(cycleEvent.ToList());
        }

        // GET: CycleEvents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CycleEvent cycleEvent = db.CycleEvent.Find(id);
            if (cycleEvent == null)
            {
                return HttpNotFound();
            }
            return View(cycleEvent);
        }

        // GET: CycleEvents/Create
        public ActionResult Create()
        {
            ViewBag.Route = db.CycleRoute.ToList();
            ViewBag.Badge = db.Badge.ToList();
            return View();
        }

        // POST: CycleEvents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CycleEvent cycleEvent)
        {
            if (ModelState.IsValid)
            {
                cycleEvent.MakerId = 1;
                db.CycleEvent.Add(cycleEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Route = db.CycleRoute.ToList();
            ViewBag.Badge = db.Badge.ToList();
            return View(cycleEvent);
        }

        // GET: CycleEvents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CycleEvent cycleEvent = db.CycleEvent.Find(id);
            if (cycleEvent == null)
            {
                return HttpNotFound();
            }
            ViewBag.Route = db.CycleRoute.ToList();
            ViewBag.Badge = db.Badge.ToList();
            return View(cycleEvent);
        }

        // POST: CycleEvents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CycleEvent cycleEvent)
        {
            if (ModelState.IsValid)
            {
                cycleEvent.MakerId = 1;
                db.Entry(cycleEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Route = db.CycleRoute.ToList();
            ViewBag.Badge = db.Badge.ToList();
            return View(cycleEvent);
        }
        [HttpGet]
        public ActionResult Join(int id)
        {
            if (id != 0)
            {
                if (db.CycleEvent.Any(e => e.Id == id))
                {
                    try
                    {
                        var game = db.CycleEvent.First(e => e.Id == id);
                        if (!game.Deelnemers.Any(e => e.Id == 1))
                        {
                            var User = db.User.First(e => e.Id == 1);
                            game.Deelnemers.Add(User);
                            db.SaveChanges();
                        }

                        return RedirectToAction("Details", "CycleEvents", new { id = id });
                    }
                    catch (Exception)
                    {
                        return RedirectToAction("Index");
                    }

                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Unsubscribe(int EventId, int UserId)
        {
            if (EventId != 0)
            {
                if (db.CycleEvent.Any(e => e.Id == EventId))
                {
                    try
                    {
                        var game = db.CycleEvent.First(e => e.Id == EventId);
                        if (game.Deelnemers.Any(e => e.Id == UserId))
                        {
                            var User = db.User.First(e => e.Id == UserId);
                            game.Deelnemers.Remove(User);
                            db.Entry(game).State = EntityState.Modified;
                            db.SaveChanges();
                        }

                        return RedirectToAction("Details", "CycleEvents", new { id = EventId });
                    }
                    catch (Exception)
                    {
                        return RedirectToAction("Index");
                    }

                }
            }
            return RedirectToAction("Index");
        }

        // GET: CycleEvents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CycleEvent cycleEvent = db.CycleEvent.Find(id);
            if (cycleEvent == null)
            {
                return HttpNotFound();
            }
            return View(cycleEvent);
        }

        // POST: CycleEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CycleEvent cycleEvent = db.CycleEvent.Find(id);
            db.CycleEvent.Remove(cycleEvent);
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
