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
    public class ReviewsController : Controller
    {
        private STBDContext db = new STBDContext();

        // GET: Reviews
        public ActionResult Index()
        {
            var review = db.Review.Include(r => r.CycleEvent).Include(r => r.CycleRoute).Include(r => r.User);
            return View(review.ToList());
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Review.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            ViewBag.EventId = new SelectList(db.CycleEvent, "Id", "EventName");
            ViewBag.RouteId = new SelectList(db.CycleRoute, "Id", "RouteName");
            ViewBag.MakerId = new SelectList(db.User, "Id", "Email");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Rating,Description,MakerId,EventId,RouteId")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Review.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventId = new SelectList(db.CycleEvent, "Id", "EventName", review.EventId);
            ViewBag.RouteId = new SelectList(db.CycleRoute, "Id", "RouteName", review.RouteId);
            ViewBag.MakerId = new SelectList(db.User, "Id", "Email", review.MakerId);
            return View(review);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Review.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventId = new SelectList(db.CycleEvent, "Id", "EventName", review.EventId);
            ViewBag.RouteId = new SelectList(db.CycleRoute, "Id", "RouteName", review.RouteId);
            ViewBag.MakerId = new SelectList(db.User, "Id", "Email", review.MakerId);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Rating,Description,MakerId,EventId,RouteId")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(db.CycleEvent, "Id", "EventName", review.EventId);
            ViewBag.RouteId = new SelectList(db.CycleRoute, "Id", "RouteName", review.RouteId);
            ViewBag.MakerId = new SelectList(db.User, "Id", "Email", review.MakerId);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Review.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Review.Find(id);
            db.Review.Remove(review);
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
