using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteMVC.Controllers;
using WebsiteMVC.Models;

namespace WebsiteMVC.Controllers
{
    public class ChucVuController : AdminController
    {
        public override eRole[] GetRoles() => new eRole[] { eRole.Admin, eRole.CanBoQuanLy };
        // GET: HocHam
        public ActionResult Index()
        {
            return View(new TeachingScheduleEntities().ChucVus.ToList().Where(q=>q.Active != false));
        }

        public ActionResult Edit(int? id)
        {
            return View(new TeachingScheduleEntities().ChucVus.ToList().FirstOrDefault(q => q.MaCV == (id ?? 0) && q.Active != false));
        }

        [HttpPost]
        public ActionResult Edit(ChucVu obj)
        {
            var db = new TeachingScheduleEntities();
            if(obj.MaCV == 0)
            {
                obj.Active = true;
                db.ChucVus.Add(obj);
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
            if(db.ChucVus.FirstOrDefault(q=>q.MaCV == id) is ChucVu value)
            {
                value.Active = false;
            }

            return Json(db.SaveChanges());
        }
    }
}