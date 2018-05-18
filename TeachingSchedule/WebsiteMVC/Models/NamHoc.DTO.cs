using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteMVC.Models
{
    public partial class NamHoc
    {
        public string mNamHoc { get => $"{KyHoc} - {NamHoc1}"; }
    }

    public partial class DNDoiGio
    {
        public LichGD LichGD2 { get => new TeachingScheduleEntities().LichGDs.Find(MaLichGD2); }
    }
}