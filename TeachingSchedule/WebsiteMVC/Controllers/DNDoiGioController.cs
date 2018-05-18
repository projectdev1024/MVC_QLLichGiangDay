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
    public class DNDoiGioController : AdminController
    {
        public override eRole[] GetRoles() => new eRole[] { };
        private TeachingScheduleEntities db = new TeachingScheduleEntities();

        public ActionResult Index(int? id)
        {
            var list = db.DNDoiGios.ToList();
            if (id.HasValue)
            {
                list = list.Where(q => q.MaLichGD == id).ToList();
            }
            if (LoginHelper.IsRole(eRole.GiangVien))
            {
                list = list.Where(q => q.LichGD.PCGD.MaGV == Account.MaGV).ToList();
            }
            return View(list);
        }

        public ActionResult Edit(int? id, int? MaLichGD)
        {
            DNDoiGio obj;
            if (id == null)
            {
                obj = new DNDoiGio
                {
                    MaLichGD = MaLichGD,
                    LichGD = db.LichGDs.FirstOrDefault(q => q.MaLichGD == MaLichGD)
                };
            }
            else
            {
                obj = db.DNDoiGios.Find(id);
            }
            ViewBag.MaLichGD2s = db.LichGDs.Where(q => q.Status != "DELETE" && q.PCGD.GV.MaBoMon == Account.MaBoMon && q.PCGD.MaGV != obj.LichGD.PCGD.MaGV).ToList();
            return View(obj);
        }

        public ActionResult Export(int id)
        {
            var dicReplace = new Dictionary<string, string>();

            DNDoiGio obj = db.DNDoiGios.FirstOrDefault(q => q.MaDN == id);

            dicReplace.Add("<BoMon0>", obj.LichGD.PCGD.LopHP.MonHoc.BoMon.TenBoMon.ToUpper());

            var gvdn = db.GVs.FirstOrDefault(q => q.MaGV == obj.LichGD.PCGD.MaGV);
            dicReplace.Add("<HoTen>", gvdn.HoTen);
            dicReplace.Add("<HocHam>", obj.LichGD.PCGD.LopHP.NamHoc.DMGs.FirstOrDefault()?.HocHam.TenHocHam);
            dicReplace.Add("<BoMon>", obj.LichGD.PCGD.LopHP.MonHoc.BoMon.TenBoMon);
            dicReplace.Add("<Mon>", obj.LichGD.PCGD.LopHP.MonHoc.TenMonHoc);
            dicReplace.Add("<Lop>", obj.LichGD.PCGD.LopHP.TenLop);
            dicReplace.Add("<HocKi>", obj.LichGD.PCGD.LopHP.NamHoc.KyHoc);
            dicReplace.Add("<NamHoc>", obj.LichGD.PCGD.LopHP.NamHoc.NamHoc1);

            var lich2 = obj.LichGD2;
            dicReplace.Add("<GV2>", lich2.PCGD.GV.HoTen);
            dicReplace.Add("<HocHam2>", lich2.PCGD.LopHP.NamHoc.DMGs.FirstOrDefault()?.HocHam.TenHocHam);
            dicReplace.Add("<BoMon2>", lich2.PCGD.LopHP.MonHoc.BoMon.TenBoMon);
            dicReplace.Add("<Mon2>", lich2.PCGD.LopHP.MonHoc.TenMonHoc);
            dicReplace.Add("<lop2>", lich2.PCGD.LopHP.TenLop);
            dicReplace.Add("<LichDoiGio>", $@"
                GV đề nghị: {gvdn.HoTen} - Môn học: {obj.LichGD.PCGD.LopHP.MonHoc.TenMonHoc} - Thứ: {obj.LichGD.Thu} - Tiết: {obj.LichGD.Tiet} 
                GV được đề nghị: {lich2.PCGD.GV.HoTen} - Môn học: {lich2.PCGD.LopHP.MonHoc.TenMonHoc} -Thứ: {lich2.Thu} - Tiết: {lich2.Tiet}");

            dicReplace.Add("<dd>", DateTime.Now.Day.ToString());
            dicReplace.Add("<mm>", DateTime.Now.Month.ToString());
            dicReplace.Add("<yyyy>", DateTime.Now.Year.ToString());

            string file = Server.MapPath("~/Content/word/DoiGio.docx");
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
                    string nameFile = string.Format("DeNghiDoiGio.docx");
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nameFile);
                }
            }
        }

        [HttpPost]
        public ActionResult Edit(DNDoiGio obj)
        {
            if (obj.MaDN > 0)
            {
                db.Entry(obj).State = EntityState.Modified;
            }
            else
            {
                obj.NgayTao = DateTime.Now;
                db.DNDoiGios.Add(obj);
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
