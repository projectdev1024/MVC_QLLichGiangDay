using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteMVC.Controllers;

namespace WebsiteMVC.Models
{
    public class LoginHelper
    {
        public static string LOGIN = "LOGIN";

        public static bool CheckLogin(string Username, string Password)
        {
            var acc = new TeachingScheduleEntities().GVs.FirstOrDefault(q => q.TenDangNhap == Username && q.MatKhau == Password && q.Active != false);
            HttpContext.Current.Session[LOGIN] = acc;
            return acc != null;
        }

        public static GV GetAccount()
        {
            return HttpContext.Current.Session[LOGIN] as GV;
        }

        public static void Logout()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session[LOGIN] = null;
        }

        public static bool IsRole(params eRole[] role)
        {
            var acc = GetAccount();
            if (acc != null)
            {
                return acc.QuyenHan == "Admin" || role.Any(q => q.ToString() == acc.QuyenHan);
            }
            return false;
        }
    }
}