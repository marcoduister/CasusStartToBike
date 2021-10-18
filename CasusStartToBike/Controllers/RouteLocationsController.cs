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
    public class RouteLocationsController : Controller
    {
        private STBDContext db = new STBDContext();

        // GET: RouteLocations
        public ActionResult Index()
        {
            var routeLocation = db.RouteLocation.Include(r => r.CycleRoute).Include(r => r.RouteLocation2);
            return View(routeLocation.ToList());
        }

        // GET: RouteLocations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RouteLocation routeLocation = db.RouteLocation.Find(id);
            if (routeLocation == null)
            {
                return HttpNotFound();
            }
            return View(routeLocation);
        }

        // GET: RouteLocations/Create
        public ActionResult Create()
        {
            ViewBag.RouteId = new SelectList(db.CycleRoute, "Id", "RouteName");
            ViewBag.LastLocationId = new SelectList(db.RouteLocation, "Id", "Location");
            return View();
        }

        // POST: RouteLocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RouteId,Location,LastLocationId")] RouteLocation routeLocation)
        {
            if (ModelState.IsValid)
            {
                db.RouteLocation.Add(routeLocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RouteId = new SelectList(db.CycleRoute, "Id", "RouteName", routeLocation.RouteId);
            ViewBag.LastLocationId = new SelectList(db.RouteLocation, "Id", "Location", routeLocation.LastLocationId);
            return View(routeLocation);
        }

        // GET: RouteLocations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RouteLocation routeLocation = db.RouteLocation.Find(id);
            if (routeLocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.RouteId = new SelectList(db.CycleRoute, "Id", "RouteName", routeLocation.RouteId);
            ViewBag.LastLocationId = new SelectList(db.RouteLocation, "Id", "Location", routeLocation.LastLocationId);
            return View(routeLocation);
        }

        // POST: RouteLocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RouteId,Location,LastLocationId")] RouteLocation routeLocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(routeLocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RouteId = new SelectList(db.CycleRoute, "Id", "RouteName", routeLocation.RouteId);
            ViewBag.LastLocationId = new SelectList(db.RouteLocation, "Id", "Location", routeLocation.LastLocationId);
            return View(routeLocation);
        }

        // GET: RouteLocations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RouteLocation routeLocation = db.RouteLocation.Find(id);
            if (routeLocation == null)
            {
                return HttpNotFound();
            }
            return View(routeLocation);
        }

        // POST: RouteLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RouteLocation routeLocation = db.RouteLocation.Find(id);
            db.RouteLocation.Remove(routeLocation);
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
