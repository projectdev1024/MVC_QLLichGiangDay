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
    public class CT_DMGController : AdminController
    {
        private TeachingScheduleEntities db = new TeachingScheduleEntities();

        public override eRole[] GetRoles() => new eRole[] { };

        // GET: CT_DMG
        public ActionResult Index(int MaDMG)
        {
            var lst_select = db.CT_DMG.Where(q => q.MaDMG == MaDMG);
            var lst = new List<CT_DMG>();
            foreach (var item in db.ChucVus.Where(q => q.Active != false))
            {
                lst.Add(new CT_DMG
                {
                    ChucVu = item,
                    MaCV = item.MaCV,
                    DMG = null,
                    IDCT_DMG = (lst_select.FirstOrDefault(q => q.MaCV == item.MaCV)?.IDCT_DMG) ?? 0,
                    MaDMG = MaDMG
                });
            }
            return View(lst.OrderByDescending(q => q.IDCT_DMG).ThenBy(q => q.ChucVu.HeSo).ToList());
        }

        [HttpPost]
        public ActionResult Index(CT_DMG[] array, int? MaDMG)
        {
            foreach (var item in array)
            {
                if (item.IDCT_DMG > 0 && item.MaCV == null)
                {
                    db.Entry(item).State = EntityState.Deleted;
                }
                else if (item.IDCT_DMG == 0 && item.MaCV > 0)
                {
                    db.CT_DMG.Add(item);
                }
            }

            db.SaveChanges();

            var obj = db.DMGs.Find(MaDMG);
            obj.TongHeSo = db.CT_DMG.Where(q => q.MaDMG == obj.MaDMG).Sum(q => q.ChucVu.HeSo) ?? 0;
            if (obj.TongHeSo > 50) obj.TongHeSo = 50;
            obj.TongDMG = db.HocHams.Find(obj.MaHocHam).DMG * (100 - obj.TongHeSo) / 100;
            db.SaveChanges();
            return RedirectToAction("Detail", "GV", new { id = obj.MaGV, tab = 3 });
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
