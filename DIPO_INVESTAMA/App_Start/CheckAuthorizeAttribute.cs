using DIPO_INVESTAMA.Entity;
using DIPO_INVESTAMA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DIPO_INVESTAMA.App_Start
{
    public class CheckAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            DIPO_INVESTAMAEntities _db = new DIPO_INVESTAMAEntities();
            //skip if AllowAnonymous
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)) return;

            try
            {
                if (String.IsNullOrEmpty(SessionManager.userId()))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login", returnUrl = filterContext.HttpContext.Request.FilePath }));
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.Roles))
                    {
                        if (_db.sp_MenuRestrictionByUserId(SessionManager.userId(), this.Roles).ToList().Count == 0)
                        {
                            filterContext.Result = new RedirectToRouteResult(
                                                    new RouteValueDictionary
                                                    {
                                                        { "controller", "Home" },
                                                        { "action", "Error403" }
                                                    });
                        }
                    }
                }

            }
            catch (Exception)
            {
                filterContext.Result = new RedirectToRouteResult(
                                                   new RouteValueDictionary
                                                    {
                                                        { "controller", "Home" },
                                                        { "action", "Error403" }
                                                    });

            }
        }
    }
}