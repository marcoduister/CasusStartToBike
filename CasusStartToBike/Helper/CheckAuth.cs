using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CasusStartToBike.Helper
{
    public class CheckAuth : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["UserID"] == null || !HttpContext.Current.Request.IsAuthenticated)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.StatusCode = 302;
                    filterContext.HttpContext.Response.End();
                }
                else
                {
                    filterContext.Result = new RedirectResult(System.Web.Security.FormsAuthentication.LoginUrl);
                }
            }
            else
            {
                var Rolen = Roles.Split(',').ToList();
                if (!Rolen.Contains(HttpContext.Current.Session["UserRole"].ToString()))
                {
                    if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                    {
                        filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                    }
                    else
                    {
                        base.HandleUnauthorizedRequest(filterContext);
                    }
                }
            }
        }
    }
}