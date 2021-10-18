using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CasusStartToBike.Data;
using CasusStartToBike.Models;

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
            ViewBag.BadgeId = new SelectList(db.Badge, "Id", "BadgeName");
            ViewBag.RouteId = new SelectList(db.CycleRoute, "Id", "RouteName");
            ViewBag.MakerId = new SelectList(db.User, "Id", "Email");
            return View();
        }

        // POST: CycleEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EventName,EventStart,EventEnd,Location,Difficulty,IsArchived,IsPublic,MakerId,BadgeId,RouteId")] CycleEvent cycleEvent)
        {
            if (ModelState.IsValid)
            {
                db.CycleEvent.Add(cycleEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BadgeId = new SelectList(db.Badge, "Id", "BadgeName", cycleEvent.BadgeId);
            ViewBag.RouteId = new SelectList(db.CycleRoute, "Id", "RouteName", cycleEvent.RouteId);
            ViewBag.MakerId = new SelectList(db.User, "Id", "Email", cycleEvent.MakerId);
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
            ViewBag.BadgeId = new SelectList(db.Badge, "Id", "BadgeName", cycleEvent.BadgeId);
            ViewBag.RouteId = new SelectList(db.CycleRoute, "Id", "RouteName", cycleEvent.RouteId);
            ViewBag.MakerId = new SelectList(db.User, "Id", "Email", cycleEvent.MakerId);
            return View(cycleEvent);
        }

        // POST: CycleEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EventName,EventStart,EventEnd,Location,Difficulty,IsArchived,IsPublic,MakerId,BadgeId,RouteId")] CycleEvent cycleEvent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cycleEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BadgeId = new SelectList(db.Badge, "Id", "BadgeName", cycleEvent.BadgeId);
            ViewBag.RouteId = new SelectList(db.CycleRoute, "Id", "RouteName", cycleEvent.RouteId);
            ViewBag.MakerId = new SelectList(db.User, "Id", "Email", cycleEvent.MakerId);
            return View(cycleEvent);
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
