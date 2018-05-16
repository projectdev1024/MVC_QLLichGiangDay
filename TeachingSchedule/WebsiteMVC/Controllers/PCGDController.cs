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
    public class PCGDController : AdminController
    {
        private TeachingScheduleEntities db = new TeachingScheduleEntities();

        // GET: AdminCP/PCGD
        public ActionResult Index(int? MaHP)
        {
            ViewBag.MaHP = db.LopHPs.Find(MaHP);
            return View(db.PCGDs.Where(q => q.MaHP == MaHP));
        }

        public ActionResult DMG_GV(int? IDNamHoc)
        {
            var lst = new List<DMG_GV>();
            var dmg = db.DMGs.Where(q => q.IDNamHoc == IDNamHoc && q.Active != false).ToList();

            foreach (var item in dmg)
            {
                lst.Add(new DMG_GV
                {
                    DMG = item,
                    TaiTT = db.PCGDs.Where(q => q.MaGV == item.MaGV).Sum(q => q.LopHP.SoTietQuyChuan) ?? 0,
                });
            }
            lst.OrderBy(q => q.TaiVuot);
            return PartialView(lst);
        }

        // GET: AdminCP/PCGD/Edit/5
        public ActionResult Edit(int? id, int? MaHP)
        {
            PCGD pCGD;
            if (id == null)
            {
                pCGD = new PCGD()
                {
                    MaHP = MaHP
                };
            }
            else
            {
                pCGD = db.PCGDs.Find(id);
            }
            ViewBag.MaGVs = new SelectList(db.GVs, "MaGV", "HoTen", pCGD.MaGV);
            return View(pCGD);
        }

        // POST: AdminCP/PCGD/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PCGD pCGD)
        {
            if (pCGD.MaPCGD > 0)
            {
                db.Entry(pCGD).State = EntityState.Modified;
            }
            else
            {
                db.PCGDs.Add(pCGD);
            }
            db.SaveChanges();
            return RedirectToAction("Index", "LopHP", new { db.LopHPs.Find(pCGD.MaHP).IDNamHoc });
        }

        // GET: AdminCP/PCGD/Delete/5
        public JsonResult Delete(int? id)
        {
            PCGD pCGD = db.PCGDs.Find(id);
            pCGD.Status = "DELETE";
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

        public override eRole[] GetRoles() => new eRole[] { };

    }
}
