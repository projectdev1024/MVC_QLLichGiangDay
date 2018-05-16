using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteMVC.Controllers;
using WebsiteMVC.Models;

namespace WebsiteMVC.Controllers
{
    public class MonHocController : AdminController
    {
        public override eRole[] GetRoles() => new eRole[] { eRole.Admin, eRole.CanBoQuanLy };
        // GET: HocHam
        public ActionResult Index()
        {
            return View(new TeachingScheduleEntities().MonHocs.ToList().Where(q => q.Active != false));
        }

        public ActionResult Edit(int? id)
        {
            var db = new TeachingScheduleEntities();
            ViewBag.BoMons = db.BoMons.ToList().Where(q => q.Active != false).CreateSelectList(q => q.MaBoMon, q => q.TenBoMon);
            return View(new TeachingScheduleEntities().MonHocs.ToList().FirstOrDefault(q => q.MaMH == (id ?? 0) && q.Active != false));
        }

        [HttpPost]
        public ActionResult Edit(MonHoc obj)
        {
            var db = new TeachingScheduleEntities();
            if (obj.MaMH == 0)
            {
                obj.Active = true;
                db.MonHocs.Add(obj);
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
            if (db.MonHocs.FirstOrDefault(q => q.MaMH == id) is MonHoc value)
            {
                value.Active = false;
            }

            return Json(db.SaveChanges());
        }
    }
}