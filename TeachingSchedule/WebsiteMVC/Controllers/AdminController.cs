using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteMVC.Models;

namespace WebsiteMVC.Controllers
{
    public abstract class AdminController : Controller
    {
        public GV Account { get; private set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Account = Models.LoginHelper.GetAccount();
            if (Account == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new
                {
                    Controller = "Login",
                    Action = "Index",
                    Area = ""
                }));
            }
            else if (Account.QuyenHan != "Admin" && GetRoles().Length > 0 && GetRoles().Any(q => q.ToString() == Account.QuyenHan) == false)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new
                {
                    Controller = "Notification",
                    Action = "NotPermistion",
                    Area = ""
                }));
            }
            base.OnActionExecuting(filterContext);
        }

        public abstract eRole[] GetRoles();
    }
}