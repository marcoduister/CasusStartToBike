using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CasusStartToBike.Helper;
using CasusStartToBike.Models;
using CasusStartToBike.Data;
using CasusStartToBike.ViewModels.Access;

namespace CasusStartToBike.Controllers
{
    public class AccessController : Controller
    {
        private STBDContext DBContext = new Data.STBDContext();


        // GET: Access/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (!Request.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return View();
            }
        }

        // POST: Access/Login
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                if (validateLogin(user.Email, user.Password))
                {
                    User currentUser = GetUserInfo(user.Email, user.Password);
                    // Clear any lingering authencation data    
                    FormsAuthentication.SignOut();

                    // Write the authentication cookie    
                    FormsAuthentication.SetAuthCookie(currentUser.Email.ToString(), false);

                    Session["UserID"] = currentUser.Id.ToString();
                    Session["UserEmail"] = currentUser.Email.ToString();
                    Session["UserName"] = currentUser.Account.FirstName + " " + currentUser.Account.LastName.ToString();
                    Session["UserRole"] = currentUser.User_Role.ToString();


                    return RedirectToAction("Index", "Home");
                }
            }

            return View(user);
        }

        // GET: Access/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: Access/Register
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel NewUser)
        {
            if (ModelState.IsValid)
            {
                if (NewUser.Password == NewUser.PasswordCheck)
                {
                    byte[] bytes = { 0, 0, 0, 25 };
                    Account AddAccount = new Account()
                    {
                        FirstName = NewUser.FirstName,
                        LastName = NewUser.LastName,
                        Birthdate = DateTime.Now
                    };
                    User Adduser = new User()
                    {
                        Email = NewUser.Email,
                        Password = NewUser.Password,
                        User_Role = Enums.Rollen.User,
                        Account = AddAccount
                    };

                    if (ValidRegister(Adduser))
                    {
                        return RedirectToAction("Login", "Access");
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(NewUser.Email), "Email is al in gebruik!!");
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(NewUser.PasswordCheck), "De wachtworden komen niet over een met elkaar");
                }
            }

            return View(NewUser);
        }

        // GET: Access/PasswordReset
        [HttpGet]
        [AllowAnonymous]
        public ActionResult PasswordReset()
        {
            return View();
        }

        // POST: Access/PasswordReset
        [HttpPost]
        [AllowAnonymous]
        public ActionResult PasswordReset(User user)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }

            return View(user);
        }

        [HttpGet]
        [CheckAuth(Roles = "Player,Gamemaster,Admin")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        // >>>>>>>>>>>>>>>>>>>>>>> ACCESS SERVICE <<<<<<<<<<<<<<<<<<<<<<
        public bool validateLogin(string Email, string Password)
        {
            if (DBContext.User.Any(e => e.Email == Email && e.Password == Password))
            {
                return true;
            }

            return false;
        }

        public User GetUserInfo(string Email, string Password)
        {
            User ReturnUser = DBContext.User.First(e => e.Email == Email && e.Password == Password);
            return ReturnUser;
        }

        public bool ValidRegister(User Adduser)
        {
            if (!DBContext.User.Any(e => e.Email == Adduser.Email))
            {
                DBContext.User.Add(Adduser);
                DBContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}