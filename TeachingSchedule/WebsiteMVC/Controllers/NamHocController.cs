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
    public class NamHocController : AdminController
    {
        private TeachingScheduleEntities db = new TeachingScheduleEntities();

        public override eRole[] GetRoles() => new eRole[] { eRole.Admin, eRole.CanBoQuanLy };

        public ActionResult Index()
        {
            var lst = db.NamHocs.ToList().Where(q => q.Active != false);
            ViewBag.add = lst.Any(q => q.TrangThai != "CLOSED") == false;
            return View(lst);
        }

        public ActionResult Edit(int? id)
        {
            var obj = db.NamHocs.ToList().FirstOrDefault(q => q.IDNamHoc == (id ?? 0) && q.Active != false);
            if (obj != null)
            {
                obj.KyHoc = obj.KyHoc?.Replace("Kỳ ", "");
                obj.NamHoc1 = obj.NamHoc1?.Replace("Năm học ", "");
            }
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(NamHoc obj)
        {
            obj.KyHoc = $"Kỳ {obj.KyHoc}";
            obj.NamHoc1 = $"Năm học {obj.NamHoc1}";
            if (obj.IDNamHoc == 0)
            {
                obj.Active = true;
                obj.TrangThai = "INIT";
                db.NamHocs.Add(obj);
            }
            else
            {
                db.Entry(obj).State = EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult Delete(int id)
        {
            if (db.NamHocs.FirstOrDefault(q => q.IDNamHoc == id) is NamHoc value)
            {
                value.Active = false;
            }

            return Json(db.SaveChanges());
        }

        public JsonResult Status(int id, string status)
        {
            if (db.NamHocs.FirstOrDefault(q => q.IDNamHoc == id) is NamHoc value)
            {
                value.TrangThai = status;
            }

            return Json(db.SaveChanges());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
