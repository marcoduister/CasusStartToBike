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
using CasusStartToBike.ViewModels;
using Newtonsoft.Json.Linq;

namespace CasusStartToBike.Controllers
{
    public class CycleRoutesController : Controller
    {
        private STBDContext db = new STBDContext();

        // GET: CycleRoutes
        public ActionResult Index()
        {
            var cycleRoute = db.CycleRoute.Include(c => c.Badge);
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
            List<RouteLocation> route = db.RouteLocation.Where(e=>e.RouteId == id).ToList();
            string routecollect = "";
            foreach (var item in route)
            {
                routecollect += item.Location;
            }
            ViewBag.Routecollect = routecollect;
            return View(cycleRoute);
        }

        // GET: CycleRoutes/Create
        public ActionResult Create()
        {
            ViewBag.BadgeId = new SelectList(db.Badge, "Id", "BadgeName");
            ViewBag.MakerId = new SelectList(db.User, "Id", "Email");
            var Model = new CycleRoute();

            return View(Model);
        }

        // POST: CycleRoutes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CycleRoute cycleRoute)
        {
            if (ModelState.IsValid)
            {
                
                List<RouteLocation> RouteList = new List<RouteLocation>();
                var locations = Request.Params["latLng"].Split('{');

                int userid = int.Parse(Session["UserID"].ToString());
                CycleRoute cycleroute = new CycleRoute()
                {
                    IsPublic = cycleRoute.IsPublic,
                    RouteName = cycleRoute.RouteName,
                    BadgeId = cycleRoute.BadgeId,
                    Difficulty = cycleRoute.Difficulty,
                    MakerId = userid,
                    RouteType = cycleRoute.RouteType
                };

                using (var context = new STBDContext())
                {
                    context.CycleRoute.Add(cycleroute);
                    context.SaveChanges();
                }

                    int RouteId = cycleroute.Id; // Yes it's here
                    int LastLocationId = 0;
                    foreach (var item in locations)
                    {
                        
                        var location = "{" + item;
                        if (location != "{")
                        {
                            try
                            {
                                RouteLocation Location = new RouteLocation()
                                {
                                    RouteId = RouteId,
                                    Location = location,
                                };
                                if (LastLocationId != 0 && locations.First() != item)
                                {
                                    Location.LastLocationId = LastLocationId;
                                }
                                db.RouteLocation.Add(Location);
                                db.SaveChanges();
                                LastLocationId = Location.Id;
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                            
                        }
                    }
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
            var Model = db.CycleRoute.Find(id);
            
            if (Model == null)
            {
                return HttpNotFound();
            }
            List<RouteLocation> route = db.RouteLocation.Where(e => e.RouteId == id).ToList();
            string routecollect = "";
            foreach (var item in route)
            {
                routecollect += item.Location;
            }
            ViewBag.Routecollect = routecollect;
            ViewBag.Badge = db.Badge.ToList();
            return View(Model);
        }

        // POST: CycleRoutes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CycleRoute Model)
        {
            List<RouteLocation> RouteList = new List<RouteLocation>();
            var locations = Request.Params["latLng"].Split('{');
            try
            {
                int userid = int.Parse(Session["UserID"].ToString());
                CycleRoute cycleroute = new CycleRoute()
                {
                    IsPublic = Model.IsPublic,
                    RouteName = Model.RouteName,
                    BadgeId = Model.BadgeId,
                    Difficulty = Model.Difficulty,
                    MakerId = userid,
                    RouteType = Model.RouteType
                };
                Model.MakerId = userid;

                db.Entry(Model).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
            
            db.RouteLocation.RemoveRange(db.RouteLocation.Where(x => x.RouteId == Model.Id));

            int LastLocationId = 0;
            foreach (var item in locations)
            {

                var location = "{" + item;
                if (location != "{")
                {
                    try
                    {
                        RouteLocation Location = new RouteLocation()
                        {
                            RouteId = Model.Id,
                            Location = location,
                        };
                        if (LastLocationId != 0 && locations.First() != item)
                        {
                            Location.LastLocationId = LastLocationId;
                        }
                        db.RouteLocation.Add(Location);
                        db.SaveChanges();
                        LastLocationId = Location.Id;
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                }
            }
            if (ModelState.IsValid)
            {
                
                return RedirectToAction("Index");
            }
            ViewBag.BadgeId = new SelectList(db.Badge, "Id", "BadgeName", Model.BadgeId);
            ViewBag.MakerId = new SelectList(db.User, "Id", "Email", Model.MakerId);
            return View(Model);
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

            db.RouteLocation.RemoveRange(db.RouteLocation.Where(x => x.RouteId == id));
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

        [HttpPost]
        public void SetLocation(FormCollection formCollection)
        {
            string hahaha = formCollection["latLng"];

        }
    }
}
