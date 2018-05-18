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
    public class NCKHController : AdminController
    {
        public override eRole[] GetRoles() => new eRole[] { };

        private TeachingScheduleEntities db = new TeachingScheduleEntities();

        // GET: AdminCP/NCKHs
        public ActionResult Index(int id)
        {
            ViewBag.MaGV = id;
            var nCKHs = db.NCKHs.Where(n => n.MaGV == id);
            return View(nCKHs.ToList());
        }

        // GET: AdminCP/NCKHs/Edit/5
        public ActionResult Edit(int? id, int MaGV)
        {
            NCKH nCKH = new NCKH()
            {
                MaGV = MaGV
            };
            if (id != null)
            {
                nCKH = db.NCKHs.Find(id);
                if (nCKH == null)
                {
                    return HttpNotFound();
                }
            }
            return View(nCKH);
        }

        [HttpPost]
        public ActionResult Edit(NCKH nCKH)
        {
            var files = Request.Files;
            if (files.Count == 1)
            {
                var file = files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var name_file = $"{DateTime.Now.ToString("hhmmssddMMyyyy")}_{Path.GetFileName(file.FileName)}";
                    var path = "/Content/Upload/" + name_file;
                    file.SaveAs(Server.MapPath(path));
                    nCKH.TaiLieu = path;
                }
            }

            if (nCKH.MaNCKH > 0)
            {
                db.Entry(nCKH).State = EntityState.Modified;
            }
            else
            {
                db.NCKHs.Add(nCKH);
            }
            db.SaveChanges();
            return RedirectToAction("Detail", "GV", new { id = nCKH.MaGV, tab = 2 });
        }

        // GET: AdminCP/NCKHs/Delete/5
        public JsonResult Delete(int? id)
        {
            NCKH nCKH = db.NCKHs.Find(id);
            db.NCKHs.Remove(nCKH);
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
