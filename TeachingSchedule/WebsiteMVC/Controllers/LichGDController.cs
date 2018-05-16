using ClosedXML.Excel;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteMVC.Controllers;
using WebsiteMVC.Models;

namespace WebsiteMVC.Controllers
{
    public class LichGDController : AdminController
    {
        public override eRole[] GetRoles() => new eRole[] { };

        private TeachingScheduleEntities db = new TeachingScheduleEntities();

        public ActionResult TKB(int? MaGV, int? IDNamHoc, int? exportExcel = 0)
        {
            var lstGV = new List<GV>();
            if (Account.QuyenHan == "GiangVien")
            {
                lstGV.Add(Account);
                MaGV = Account.MaGV;
            }
            else
            {
                lstGV = db.GVs.Where(q => q.Active != false).ToList();
            }
            var IDNamHocs = db.NamHocs.Where(q => q.Active != false).ToList();

            ViewBag.MaGVs = lstGV.CreateSelectList(q => q.MaGV, q => q.HoTen, MaGV);
            if (IDNamHoc == null) IDNamHoc = IDNamHocs.FirstOrDefault()?.IDNamHoc;
            ViewBag.IDNamHocs = IDNamHocs.CreateSelectList(q => q.IDNamHoc, q => q.mNamHoc, IDNamHoc);
            var namhoc = db.NamHocs.Find(IDNamHoc);
            ViewBag.NamHoc = namhoc;

            var lstGD = db.LichGDs.Where(q => q.PCGD.LopHP.IDNamHoc == IDNamHoc).ToList();
            if (MaGV.HasValue) lstGD = lstGD.Where(q => q.PCGD.MaGV == MaGV).ToList();
            if (exportExcel == 1)
            {
                return new PartialViewAsPdf("TKBPartial", lstGD)
                {
                    FileName = $"TKB {namhoc.mNamHoc}.pdf",
                    PageOrientation = Rotativa.Options.Orientation.Landscape,
                    ContentDisposition = Rotativa.Options.ContentDisposition.Attachment,
                    PageSize = Rotativa.Options.Size.Letter
                };
            }
            return View(lstGD);
        }

        // GET: AdminCP/LichGD
        public ActionResult Index(int MaPCGD)
        {
            var lst = db.LichGDs.Where(l => l.Status != "DELETE" && l.MaPCGD == MaPCGD).ToList();
            if (Account.QuyenHan != "Admin" && LoginHelper.IsRole(eRole.GiangVien))
            {
                lst = lst.Where(q => q.PCGD.MaGV == Account.MaGV).ToList();
            }

            ViewBag.PCGD = db.PCGDs.Find(MaPCGD);
            return View(lst);
        }


        // GET: AdminCP/LichGD/Edit/5
        public ActionResult Edit(int? id, int? MaPCGD)
        {
            LichGD lichGD;
            if (id == null)
            {
                lichGD = new LichGD()
                {
                    MaPCGD = MaPCGD
                };
            }
            else
            {
                lichGD = db.LichGDs.Find(id);
            }
            return View(lichGD);
        }

        [HttpPost]
        public ActionResult Edit(LichGD lichGD)
        {
            if (lichGD.MaLichGD > 0)
            {
                db.Entry(lichGD).State = EntityState.Modified;
            }
            else
            {
                db.LichGDs.Add(lichGD);
            }
            db.SaveChanges();
            return RedirectToAction("Index", new { MaPCGD = lichGD.MaPCGD });
        }

        public JsonResult Delete(int id)
        {
            LichGD lichGD = db.LichGDs.Find(id);
            lichGD.Status = "DELETE";
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
