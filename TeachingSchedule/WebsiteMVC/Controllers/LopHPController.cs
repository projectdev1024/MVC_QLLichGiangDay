using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using WebsiteMVC.Controllers;
using WebsiteMVC.Models;

namespace WebsiteMVC.Controllers
{
    public class LopHPController : AdminController
    {
        public override eRole[] GetRoles() => new eRole[] { };
        private TeachingScheduleEntities db = new TeachingScheduleEntities();

        // GET: AdminCP/LopHP
        public ActionResult Index(int? IDNamHoc, int? IDBoMon, string submit = null)
        {
            if (submit == "Export") return Export(IDNamHoc);

            var namhoc = db.NamHocs.Where(q => q.Active != false).OrderBy(q => q.TrangThai).ToList();
            if (IDNamHoc.HasValue == false)
            {
                IDNamHoc = namhoc.Last().IDNamHoc;
            }

            ViewBag.NamHocs = namhoc.CreateSelectList(q => q.IDNamHoc, q => q.mNamHoc, IDNamHoc);
            var lopHPs = db.LopHPs.Include(l => l.MonHoc).Where(q => q.Active != false && q.IDNamHoc == IDNamHoc).ToList();
            if (Account.QuyenHan == "Admin")
            {
                ViewBag.Title = $"LỚP HỌC PHẦN - TẤT CẢ BỘ MÔN";
                ViewBag.IDBoMons = db.BoMons.Where(q => q.Active != false).CreateSelectList(q => q.MaBoMon, q => q.TenBoMon, IDBoMon);
                if (IDBoMon.HasValue) lopHPs = lopHPs.Where(q => q.MonHoc.MaBoMon == IDBoMon).ToList();
            }
            else
            {
                IDBoMon = Account.MaBoMon;
                lopHPs = lopHPs.Where(q => q.MonHoc.MaBoMon == Account.MaBoMon).ToList();
                ViewBag.Title = $"LỚP HỌC PHẦN - BỘ MÔN {Account.BoMon.TenBoMon}";
                ViewBag.MaBoMons = new List<BoMon> { Account.BoMon }.CreateSelectList(q => q.MaBoMon, q => q.TenBoMon, IDBoMon);
                ViewBag.IDBoMons = db.BoMons.Where(q => q.Active != false && q.MaBoMon == Account.MaBoMon).CreateSelectList(q => q.MaBoMon, q => q.TenBoMon, IDBoMon);
            }

            ViewBag.edit = namhoc.FirstOrDefault(q => q.IDNamHoc == IDNamHoc)?.TrangThai != "CLOSED";
            return View(lopHPs);
        }

        public ActionResult Export(int? IDNamHoc)
        {
            var namhoc = db.NamHocs.Find(IDNamHoc);
            var lst = db.LopHPs.Where(q => q.IDNamHoc == IDNamHoc).ToList();
            var wb = new XLWorkbook(Server.MapPath("/Content/Excels/lophp.xlsx"));
            string nameFile = string.Format("GD {0}.xlsx", namhoc.mNamHoc);
            var ws = wb.Worksheets.Worksheet(1);

            for (int i = 0; i < lst.Count; i++)
            {
                var item = lst[i];
                ws.Cell(2 + i, 1).Value = namhoc.KyHoc;
                ws.Cell(2 + i, 2).Value = item.MonHoc.TenMonHoc;
                ws.Cell(2 + i, 4).Value = item.SoTietTKB;
                ws.Cell(2 + i, 5).Value = item.LHDT;
                ws.Cell(2 + i, 6).Value = item.Kip;
                ws.Cell(2 + i, 7).Value = item.DiaDiem;
                ws.Cell(2 + i, 8).Value = item.HeSoKip;
                ws.Cell(2 + i, 9).Value = item.HeSoLHDT;
                ws.Cell(2 + i, 10).Value = item.HeSoQS;
                ws.Cell(2 + i, 11).Value = item.HeSoDD;
                ws.Cell(2 + i, 12).Value = item.TongHeSo;
                ws.Cell(2 + i, 13).Value = item.SoTietQuyChuan;
                ws.Cell(2 + i, 14).Value = item.TenLop;
                ws.Cell(2 + i, 15).Value = item.SiSo;
                ws.Cell(2 + i, 16).Value = item.TGTHIKT;
                ws.Cell(2 + i, 17).Value = item.HinhThucThi;
                ws.Cell(2 + i, 18).Value = item.PCGDs?.First().MaGV;
                ws.Cell(2 + i, 19).Value = item.PCGDs?.First().GV.HoTen;
            }

            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nameFile);
            }
        }

        public ActionResult Import(int? IDNamHoc)
        {
            ViewBag.IDNamHocs = db.NamHocs.Where(q => q.Active != false).CreateSelectList(q => q.IDNamHoc, q => q.mNamHoc, IDNamHoc);
            return View();
        }

        [HttpPost]
        public ActionResult Import(int? IDNamHoc, HttpPostedFileBase excel, int sheet = 1)
        {
            var workbook = new ClosedXML.Excel.XLWorkbook(excel.InputStream);
            var ws = workbook.Worksheet(sheet);
            var lst = (from row in ws.Rows(2, ws.RowsUsed().Count())
                       select GetLopHP(IDNamHoc, row)).ToList();
            lst.ForEach(q =>
            {
                if (q != null)
                {
                    q.HeSoKip = q.Kip == "N" ? 0 : 0.5;
                    q.HeSoDD = q.DiaDiem == "HN" ? 0 : 0.5;
                    switch (q.LHDT)
                    {
                        case "DH":
                            q.HeSoLHDT = 0;
                            break;
                        case "CH":
                            q.HeSoLHDT = 0.6;
                            break;
                        case "LT":
                            q.HeSoLHDT = 0.2;
                            break;
                        case "CD":
                            q.HeSoLHDT = 0;
                            break;
                        case "AT":
                            q.HeSoLHDT = 0.2;
                            break;
                        default:
                            q.HeSoLHDT = 0;
                            break;
                    }
                    if (q.SiSo <= 50) q.HeSoQS = 0;
                    else if (q.SiSo > 100) q.HeSoQS = 0.6;
                    else q.HeSoQS = Math.Ceiling(((q.SiSo ?? 0) - 50) / 10.0) * 0.1;
                    q.TongHeSo = q.HeSoDD + q.HeSoKip + q.HeSoLHDT + q.HeSoQS + 1;
                    q.SoTietQuyChuan = (int?)(q.TongHeSo * q.SoTietTKB);
                    db.LopHPs.Add(q);
                }
            });
            db.SaveChanges();
            return RedirectToAction("Index", new { IDNamHoc });
        }

        private void _fillHeSo(LopHP q)
        {
            q.HeSoKip = q.Kip == "N" ? 0 : 0.5;
            q.HeSoDD = q.DiaDiem == "HN" ? 0 : 0.5;
            switch (q.LHDT)
            {
                case "DH":
                    q.HeSoLHDT = 0;
                    break;
                case "CH":
                    q.HeSoLHDT = 0.6;
                    break;
                case "LT":
                    q.HeSoLHDT = 0.2;
                    break;
                case "CD":
                    q.HeSoLHDT = 0;
                    break;
                case "AT":
                    q.HeSoLHDT = 0.2;
                    break;
                default:
                    q.HeSoLHDT = 0;
                    break;
            }
            if (q.SiSo <= 50) q.HeSoQS = 0;
            else if (q.SiSo > 100) q.HeSoQS = 0.6;
            else q.HeSoQS = Math.Ceiling(((q.SiSo ?? 0) - 50) / 10.0) * 0.1;
            q.TongHeSo = q.HeSoDD + q.HeSoKip + q.HeSoLHDT + q.HeSoQS + 1;
            switch (q.MonHoc.M)
            {
                case "A":
                    q.SoTietQuyChuan = (int)((1 + q.HeSoKip + q.HeSoLHDT + q.HeSoDD) * (15 + Math.Max(0, (float)((q.SiSo - 20) / 4))) * q.SoTietTKB);
                    break;
                case "TT":
                    q.SoTietQuyChuan = (int)((1 + q.HeSoKip + q.HeSoLHDT + q.HeSoDD) * q.SiSo * 0.5);
                    break;
                case "M":
                default:
                    q.SoTietQuyChuan = (int?)(q.TongHeSo * q.SoTietTKB);
                    break;
            }
        }

        private LopHP GetLopHP(int? IDNamHoc, IXLRow row)
        {
            try
            {
                var smh = row.Cell("C").Value.ToString();
                var mh = db.MonHocs.FirstOrDefault(q => q.TenMonHoc.Trim().ToLower() == smh.Trim().ToLower());
                if (mh == null) return null;
                var res = new LopHP
                {
                    IDNamHoc = IDNamHoc,
                    Active = true,
                    SoTietTKB = int.Parse(row.Cell("F").Value.ToString()),
                    Kip = row.Cell("H").Value.ToString(),
                    DiaDiem = row.Cell("K").Value.ToString(),
                    TenLop = row.Cell("J").Value.ToString(),
                    MaHocPhan = row.Cell("B").Value.ToString(),
                    SiSo = int.Parse(row.Cell("I").Value.ToString()),
                };
                res.MaMH = mh.MaMH;
                DateTime dateTime = DateTime.Now;
                if (DateTime.TryParseExact(row.Cell("P").Value.ToString(), new string[] { "dd/MM/yyyy", "dd/M/yyyy" }, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dateTime))
                {
                    res.TGTHIKT = dateTime;
                }
                res.IDNamHoc = IDNamHoc;
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ActionResult Edit(int? MaHP)
        {
            LopHP lopHP;
            if (MaHP == null)
            {
                lopHP = new LopHP();
            }
            else
            {
                lopHP = db.LopHPs.Find(MaHP);
            }

            var MaBoMon = LoginHelper.GetAccount().MaBoMon;
            ViewBag.MaMH = db.MonHocs.Where(q => q.MaBoMon == MaBoMon).CreateSelectList(q => q.MaMH, q => q.TenMonHoc, lopHP.MaMH);
            ViewBag.IDNamHocs = db.NamHocs.Where(q => q.Active != false).CreateSelectList(q => q.IDNamHoc, q => q.mNamHoc, lopHP.IDNamHoc);
            return View(lopHP);
        }

        [HttpPost]
        public ActionResult Edit(LopHP lopHP)
        {
            lopHP.MonHoc = db.MonHocs.Find(lopHP.MaMH);
            _fillHeSo(lopHP);
            if (lopHP.MaHP > 0)
            {
                db.Entry(lopHP).State = EntityState.Modified;
            }
            else
            {
                db.LopHPs.Add(lopHP);
            }
            db.SaveChanges();
            return RedirectToAction("Index", new { lopHP.IDNamHoc });
        }

        public JsonResult Delete(int id)
        {
            LopHP lopHP = db.LopHPs.Find(id);
            lopHP.Active = false;
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
