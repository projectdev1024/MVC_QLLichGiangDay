using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteMVC.Models;

namespace WebsiteMVC.Controllers
{
    public class BoMonController : AdminController
    {
        public override eRole[] GetRoles() => new eRole[] { eRole.Admin, eRole.CanBoQuanLy };
        // GET: HocHam
        public ActionResult Index()
        {
            return View(new TeachingScheduleEntities().BoMons.ToList().Where(q => q.Active != false));
        }

        public ActionResult Edit(int? id)
        {
            return View(new TeachingScheduleEntities().BoMons.ToList().FirstOrDefault(q => q.MaBoMon == (id ?? 0) && q.Active != false));
        }

        [HttpPost]
        public ActionResult Edit(BoMon obj)
        {
            var db = new TeachingScheduleEntities();
            if (obj.MaBoMon == 0)
            {
                obj.Active = true;
                db.BoMons.Add(obj);
            }
            else
            {
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult Delete(int id)
        {
            var db = new TeachingScheduleEntities();
            if (db.BoMons.FirstOrDefault(q => q.MaBoMon == id) is BoMon value)
            {
                value.Active = false;
            }

            return Json(db.SaveChanges());
        }
    }
}
