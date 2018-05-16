using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteMVC.Controllers;
using WebsiteMVC.Models;

namespace WebsiteMVC.Controllers
{
    public class HocHamController : AdminController
    {
        public override eRole[] GetRoles() => new eRole[] {   };
        // GET: HocHam
        public ActionResult Index()
        {
            return View(new TeachingScheduleEntities().HocHams.ToList().Where(q=>q.Active != false));
        }

        public ActionResult Edit(int? id)
        {
            return View(new TeachingScheduleEntities().HocHams.ToList().FirstOrDefault(q => q.MaHocHam == (id ?? 0) && q.Active != false));
        }

        [HttpPost]
        public ActionResult Edit(HocHam obj)
        {
            var db = new TeachingScheduleEntities();
            if(obj.MaHocHam == 0)
            {
                obj.Active = true;
                db.HocHams.Add(obj);
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
            if(db.HocHams.FirstOrDefault(q=>q.MaHocHam == id) is HocHam value)
            {
                value.Active = false;
            }

            return Json(db.SaveChanges());
        }
    }
}