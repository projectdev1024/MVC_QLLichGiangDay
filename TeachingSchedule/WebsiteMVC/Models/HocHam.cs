//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebsiteMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HocHam
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HocHam()
        {
            this.DMGs = new HashSet<DMG>();
        }
    
        public int MaHocHam { get; set; }
        public string TenHocHam { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<double> HeSo { get; set; }
        public Nullable<int> DMG { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DMG> DMGs { get; set; }
    }
}
