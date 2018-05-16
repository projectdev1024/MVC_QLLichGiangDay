using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteMVC.Controllers
{
    public class NotificationController : Controller
    {
        public ActionResult NotPermistion()
        {
            return View();
        }
    }
}