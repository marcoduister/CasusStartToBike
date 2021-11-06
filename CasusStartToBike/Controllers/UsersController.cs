using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CasusStartToBike.Helper;
using CasusStartToBike.Models;
using CasusStartToBike.Data;
using CasusStartToBike.ViewModels;
using System.Data.Entity.Infrastructure;

namespace CasusStartToBike.Controllers
{
    public class UsersController : Controller
    {
        private STBDContext db = new STBDContext();

        // GET: Users
        [HttpGet]
        [CheckAuth(Roles = "User,Admin")]
        public ActionResult Index()
        {
            var Model = db.User.ToList();
            foreach (User user in Model.ToList())
            {
                if (user.Id == Convert.ToInt32(Session["UserID"]))
                {
                    Model.Remove(user);
                }
            }
            return View(Model);
        }

        // GET: Users/Accept/1
        [HttpGet]
        [CheckAuth(Roles = "Admin")]
        public ActionResult Accept(int id)
        {
            return View();
        }

        // GET: Users/Details/1
        [HttpGet]
        [CheckAuth(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            var Model = db.User.Find(id);
            return View(Model);
        }


        // GET: Users/Edit/1
        [HttpGet]
        [CheckAuth(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            User user = GetUserById(id);

            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        // POST: Users/Edit/5
        [HttpPost]
        [CheckAuth(Roles = "User,Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User Model)
        {
            if (ModelState.IsValid)
            {
                if (EditUser(Model))
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpPost]
        //public void SaveProfileImage(FormCollection formCollection)
        //{
        //    if (Request.Files.Count != 0)
        //    {
        //        byte[] Profileimage = null;
        //        int Id = int.Parse(formCollection["Id"]);
        //        using (var Reader = new BinaryReader(Request.Files[0].InputStream))
        //        {
        //            Profileimage = Reader.ReadBytes(Request.Files[0].ContentLength);
        //        }
        //        if (Profileimage != null)
        //        {
        //            UploadProfileImage(Profileimage, Id);
        //        }
        //    }
        //}

        // GET: Users/Delete/5
        [HttpGet]
        [CheckAuth(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost]
        [CheckAuth(Roles = "Admin")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        public User GetUserById(int Id)
        {
            if (db.User.Any(e => e.Id == Id))
            {
                User User = db.User.First(e => e.Id == Id);

                return User;
            }
            else
            {
                return null;
            }

        }

        public bool EditUser(User Model)
        {
            if (!db.User.Any(e => e.Email == Model.Email))
            {

                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        //public void UploadProfileImage(byte[] _profileimage, int Id)
        //{
        //    if (db.Account.Any(e => e.UserId == Id))
        //    {
        //        Account updateprofile = db.Account.First(e => e.UserId == Id);

        //        updateprofile.ProfileImage = _profileimage;

        //        db.SaveChanges();
        //    }
        //}

        // >>>>> PROFILE <<<<<
        [CheckAuth(Roles = "User,Admin")]
        public ActionResult Profile(int? id)
        {
            var Model = db.User.Find(id);
            return View(Model);
        }



        
        public ActionResult FollowUser(int? id)
        {
            DateTime date = DateTime.Today;

            var FId = Convert.ToInt32(id);

            Follower follower = new Follower()
            {
                UserId = Convert.ToInt32(Session["UserId"]),
                FollowerId = FId,
                Date = BitConverter.GetBytes(date.Ticks)
            };
            db.Follower.Add(follower);
            db.SaveChanges();



            return RedirectToAction("Index");



        }
    }
}
