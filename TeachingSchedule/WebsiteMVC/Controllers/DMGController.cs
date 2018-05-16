using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteMVC.Controllers;
using WebsiteMVC.Models;

namespace WebsiteMVC.Controllers
{
    public class DMGController : AdminController
    {
        public override eRole[] GetRoles() => new eRole[] { };
        private TeachingScheduleEntities db = new TeachingScheduleEntities();

        // GET: AdminCP/DMG
        public ActionResult Index(int? MaGV)
        {
            var dMGs = db.DMGs.Where(d => d.Active != false && d.GV.MaGV == MaGV).ToList();
            ViewBag.MaGV = MaGV;
            return View(dMGs);
        }

        // GET: AdminCP/DMG/Edit/5
        public ActionResult Edit(int? id, int? MaGV)
        {
            DMG dMG;
            if (id == null)
            {
                dMG = new DMG
                {
                    MaGV = MaGV,
                    TongHeSo = 0,
                    TongDMG = 0
                };
            }
            else
            {
                dMG = db.DMGs.Find(id);
                if (dMG == null)
                {
                    return HttpNotFound();
                }
            }
            ViewBag.IDNamHocs = db.NamHocs.Where(q => q.Active != false).CreateSelectList(q => q.IDNamHoc, q => q.mNamHoc, dMG.IDNamHoc);
            ViewBag.MaHocHams = db.HocHams.Where(q => q.Active != false).CreateSelectList(q => q.MaHocHam, q => q.TenHocHam, dMG.MaHocHam);
            return View(dMG);
        }

        // POST: AdminCP/DMG/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DMG obj)
        {
            if (obj.MaDMG > 0)
            {
                db.Entry(obj).State = EntityState.Modified;
            }
            else
            {
                db.DMGs.Add(obj);
            }
            obj.TongHeSo = db.CT_DMG.Where(q => q.MaDMG == obj.MaDMG).Sum(q => q.ChucVu.HeSo) ?? 0;
            if (obj?.TongHeSo > 50) obj.TongHeSo = 50;
            obj.TongDMG = db.HocHams.Find(obj.MaHocHam).DMG ?? 0;
            db.SaveChanges();
            return RedirectToAction("Detail", "GV", new { id = obj.MaGV, tab = 3 });
        }

        // POST: AdminCP/DMG/Delete/5
        public JsonResult Delete(int id)
        {
            DMG dMG = db.DMGs.Find(id);
            dMG.Active = false;
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
