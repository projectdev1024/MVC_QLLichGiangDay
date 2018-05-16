using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteMVC.Models
{
    public class DMG_GV
    {
        public DMG DMG { get; set; }
        public int TaiTT { get; set; }
        public int TaiNV { get => (DMG.TongDMG * DMG.TongHeSo / 100) ?? 0; }
        public int TaiVuot { get => TaiTT - TaiNV; }
    }
}