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
    public class FollowersController : Controller
    {
        private STBDContext db = new STBDContext();

        // GET: Followers
        public ActionResult Index()
        {
            var follower = db.Follower.Include(f => f.User).Include(f => f.User1);
            return View(follower.ToList());
        }

        // GET: Followers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Follower follower = db.Follower.Find(id);
            if (follower == null)
            {
                return HttpNotFound();
            }
            return View(follower);
        }

        // GET: Followers/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.User, "Id", "Email");
            ViewBag.FollowerId = new SelectList(db.User, "Id", "Email");
            return View();
        }

        // POST: Followers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,FollowerId,Date")] Follower follower)
        {
            if (ModelState.IsValid)
            {
                db.Follower.Add(follower);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.User, "Id", "Email", follower.UserId);
            ViewBag.FollowerId = new SelectList(db.User, "Id", "Email", follower.FollowerId);
            return View(follower);
        }

        // GET: Followers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Follower follower = db.Follower.Find(id);
            if (follower == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.User, "Id", "Email", follower.UserId);
            ViewBag.FollowerId = new SelectList(db.User, "Id", "Email", follower.FollowerId);
            return View(follower);
        }

        // POST: Followers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,FollowerId,Date")] Follower follower)
        {
            if (ModelState.IsValid)
            {
                db.Entry(follower).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.User, "Id", "Email", follower.UserId);
            ViewBag.FollowerId = new SelectList(db.User, "Id", "Email", follower.FollowerId);
            return View(follower);
        }

        // GET: Followers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Follower follower = db.Follower.Find(id);
            if (follower == null)
            {
                return HttpNotFound();
            }
            return View(follower);
        }

        // POST: Followers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Follower follower = db.Follower.Find(id);
            db.Follower.Remove(follower);
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
