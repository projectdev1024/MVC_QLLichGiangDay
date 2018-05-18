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
using Xceed.Words.NET;

namespace WebsiteMVC.Controllers
{
    public class DNDoiGVController : AdminController
    {
        public override eRole[] GetRoles() => new eRole[] { };
        private TeachingScheduleEntities db = new TeachingScheduleEntities();

        public ActionResult Index(int? id)
        {
            var list = db.DNDoiGVs.Include(d => d.PCGD);
            if (id.HasValue)
            {
                list = list.Where(q => q.MaPCGD == id);
            }
            return View(list.ToList());
        }

        public ActionResult Export(int id)
        {
            var dicReplace = new Dictionary<string, string>();

            DNDoiGV obj = db.DNDoiGVs.Find(id);

            dicReplace.Add("<BoMon0>", obj.PCGD.LopHP.MonHoc.BoMon.TenBoMon.ToUpper());

            var gvdn = db.GVs.FirstOrDefault(q => q.MaGV == obj.PCGD.MaGV);
            dicReplace.Add("<HoTen>", gvdn.HoTen);
            dicReplace.Add("<HocHam>", obj.PCGD.LopHP.NamHoc.DMGs.FirstOrDefault()?.HocHam.TenHocHam);
            dicReplace.Add("<BoMon>", obj.PCGD.LopHP.MonHoc.BoMon.TenBoMon);
            dicReplace.Add("<Mon>", obj.PCGD.LopHP.MonHoc.TenMonHoc);
            dicReplace.Add("<Lop>", obj.PCGD.LopHP.TenLop);
            dicReplace.Add("<HocKi>", obj.PCGD.LopHP.NamHoc.KyHoc);
            dicReplace.Add("<NamHoc>", obj.PCGD.LopHP.NamHoc.NamHoc1);

            var gv2 = db.GVs.Find(obj.MaGV);
            dicReplace.Add("<GV2>", gv2.HoTen);
            dicReplace.Add("<HocHam2>", gv2.DMGs.FirstOrDefault(q => q.IDNamHoc == obj.PCGD.LopHP.IDNamHoc)?.HocHam.TenHocHam);

            dicReplace.Add("<start>", obj.NgayBD?.ToString("dd-MM-yyyy"));
            dicReplace.Add("<end>", obj.NgayKT?.ToString("dd-MM-yyyy"));

            dicReplace.Add("<dd>", DateTime.Now.Day.ToString());
            dicReplace.Add("<mm>", DateTime.Now.Month.ToString("00"));
            dicReplace.Add("<yyyy>", DateTime.Now.Year.ToString());

            string file = Server.MapPath("~/Content/word/DoiGV.docx");
            using (DocX document = DocX.Load(file).Copy())
            {
                // Replace text in this document.
                foreach (var item in dicReplace)
                {
                    document.ReplaceText(item.Key, item.Value + "");
                }

                // Save changes made to this document.
                document.Save();
                using (var stream = new MemoryStream())
                {
                    document.SaveAs(stream);
                    string nameFile = string.Format("DeNghiDoiGiaoVien.docx");
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nameFile);
                }
            }
        }

        public ActionResult Edit(int? id, int? MaPCGD)
        {
            DNDoiGV obj;
            if (id == null)
            {
                obj = new DNDoiGV
                {
                    MaPCGD = MaPCGD,
                    PCGD = db.PCGDs.Find(MaPCGD)
                };
            }
            else
            {
                obj = db.DNDoiGVs.Find(id);
            }
            ViewBag.MaGVs = db.GVs.Where(q => q.Active != false && q.MaGV != obj.PCGD.MaGV && q.MaBoMon == Account.MaBoMon).CreateSelectList(q => q.MaGV, q => q.HoTen);
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DNDoiGV obj)
        {
            if (obj.MaDN > 0)
            {
                db.Entry(obj).State = EntityState.Modified;
            }
            else
            {
                obj.NgayTao = DateTime.Now;
                db.DNDoiGVs.Add(obj);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
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
