using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteMVC.Controllers;
using WebsiteMVC.Models;

namespace WebsiteMVC.Controllers
{
    public class GVController : AdminController
    {
        public override eRole[] GetRoles() => new eRole[] { };
        // GET: AdminCP/GV
        public ActionResult Index()
        {
            var lst = new List<GV>();
            var db = new Models.TeachingScheduleEntities();
            switch (Account.QuyenHan)
            {
                case "GiangVien":
                    lst.Add(Account);
                    break;
                case "Admin":
                    lst = db.GVs.ToList().Where(q => q.Active != false).ToList();
                    break;
                default:
                    lst = db.GVs.ToList().Where(q => q.Active != false && q.MaBoMon == Account.MaBoMon).ToList();
                    break;
            }
            return View(lst);
        }

        public ActionResult Detail(int id, int? tab)
        {
            ViewBag.tab = tab;
            var db = new TeachingScheduleEntities();
            ViewBag.MaBoMons = db.BoMons.Where(q => q.Active != false).CreateSelectList(q => q.MaBoMon, q => q.TenBoMon);
            return View(db.GVs.FirstOrDefault(q => q.MaGV == id));
        }

        public ActionResult Create()
        {
            var db = new TeachingScheduleEntities();
            ViewBag.MaBoMons = db.BoMons.Where(q => q.Active != false).CreateSelectList(q => q.MaBoMon, q => q.TenBoMon);
            return View(new GV());
        }

        public ActionResult Edit(int? id)
        {
            var db = new TeachingScheduleEntities();
            var ob = db.GVs.FirstOrDefault(q => q.MaGV == (id ?? 0));
            if (ob == null) return RedirectToAction("Index");
            ViewBag.MaBoMons = db.BoMons.Where(q => q.Active != false).CreateSelectList(q => q.MaBoMon, q => q.TenBoMon, ob.MaBoMon);
            return View(ob);
        }

        [HttpPost]
        public ActionResult Edit(GV obj)
        {
            var db = new TeachingScheduleEntities();
            if (obj.MaGV == 0)
            {
                db.GVs.Add(obj);
            }
            else
            {
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            }

            var files = Request.Files;
            if (files.Count == 1)
            {
                var file = files[0];
                if (file != null && file.ContentType.Contains("image") && file.ContentLength > 0)
                {
                    var name_file = $"{DateTime.Now.ToString("hhmmssddMMyyyy")}_{Path.GetFileName(file.FileName)}";
                    var path = "/Content/Upload/img/" + name_file;
                    file.SaveAs(Server.MapPath(path));
                    obj.Avatar = path;
                }
            }

            if (db.SaveChanges() == 0)
            {
                ModelState.AddModelError("", "Thêm thất bại");
                return View();
            }
            if (obj.MaGV == Account.MaGV) return RedirectToAction("Logout", "Login");
            return RedirectToAction("Index");
        }

        public JsonResult Delete(int id)
        {
            var db = new TeachingScheduleEntities();
            if (db.GVs.FirstOrDefault(q => q.MaGV == id) is GV value)
            {
                value.Active = false;
            }
            return Json(db.SaveChanges());
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(string oldpass, string newpass, string repass)
        {
            if (newpass != repass)
            {
                ModelState.AddModelError("", "Xác nhận mật khẩu không chính xác.");
            }
            if (newpass.Length < 8)
            {
                ModelState.AddModelError("", "Nhập mật khẩu có ít nhất 08 tí tự.");
            }
            if (oldpass.Length < 0)
            {
                ModelState.AddModelError("", "Vui lòng nhập mật khẩu cũ.");
            }
            if (ModelState.ContainsKey("") == false)
            {
                var db = new Models.TeachingScheduleEntities();
                var acc = db.GVs.FirstOrDefault(q => q.MaGV == Account.MaGV);
                if (acc.MatKhau == oldpass)
                {
                    acc.MatKhau = repass;
                    db.SaveChanges();
                    return RedirectToAction("Logout", "Login", new { area = "" });
                }
                ModelState.AddModelError("", "Mật khẩu cũ không chính xác");
            }
            return View();
        }

        public ActionResult MyProfile()
        {
            var db = new TeachingScheduleEntities();
            var ob = db.GVs.FirstOrDefault(q => q.MaGV == Account.MaGV);
            if (ob == null) return RedirectToAction("Index");
            ViewBag.MaBoMons = db.BoMons.Where(q => q.Active != false).CreateSelectList(q => q.MaBoMon, q => q.TenBoMon, ob.MaBoMon);
            return View(ob);
        }
    }
}