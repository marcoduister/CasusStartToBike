using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CasusStartToBike.Models;

namespace CasusStartToBike.Controllers
{
    public class CycleRoutesController : Controller
    {
        private STBDModel db = new STBDModel();

        // GET: CycleRoutes
        public ActionResult Index()
        {
            var cycleRoute = db.CycleRoute.Include(c => c.Badge).Include(c => c.User);
            return View(cycleRoute.ToList());
        }

        // GET: CycleRoutes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CycleRoute cycleRoute = db.CycleRoute.Find(id);
            if (cycleRoute == null)
            {
                return HttpNotFound();
            }
            return View(cycleRoute);
        }

        // GET: CycleRoutes/Create
        public ActionResult Create()
        {
            ViewBag.BadgeId = new SelectList(db.Badge, "Id", "BadgeName");
            ViewBag.MakerId = new SelectList(db.User, "Id", "Email");
            return View();
        }

        // POST: CycleRoutes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RouteName,RouteType,Difficulty,IsPublic,MakerId,BadgeId")] CycleRoute cycleRoute)
        {
            if (ModelState.IsValid)
            {
                db.CycleRoute.Add(cycleRoute);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BadgeId = new SelectList(db.Badge, "Id", "BadgeName", cycleRoute.BadgeId);
            ViewBag.MakerId = new SelectList(db.User, "Id", "Email", cycleRoute.MakerId);
            return View(cycleRoute);
        }

        // GET: CycleRoutes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CycleRoute cycleRoute = db.CycleRoute.Find(id);
            if (cycleRoute == null)
            {
                return HttpNotFound();
            }
            ViewBag.BadgeId = new SelectList(db.Badge, "Id", "BadgeName", cycleRoute.BadgeId);
            ViewBag.MakerId = new SelectList(db.User, "Id", "Email", cycleRoute.MakerId);
            return View(cycleRoute);
        }

        // POST: CycleRoutes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RouteName,RouteType,Difficulty,IsPublic,MakerId,BadgeId")] CycleRoute cycleRoute)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cycleRoute).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BadgeId = new SelectList(db.Badge, "Id", "BadgeName", cycleRoute.BadgeId);
            ViewBag.MakerId = new SelectList(db.User, "Id", "Email", cycleRoute.MakerId);
            return View(cycleRoute);
        }

        // GET: CycleRoutes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CycleRoute cycleRoute = db.CycleRoute.Find(id);
            if (cycleRoute == null)
            {
                return HttpNotFound();
            }
            return View(cycleRoute);
        }

        // POST: CycleRoutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CycleRoute cycleRoute = db.CycleRoute.Find(id);
            db.CycleRoute.Remove(cycleRoute);
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
