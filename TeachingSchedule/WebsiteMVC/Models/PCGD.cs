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
    
    public partial class PCGD
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PCGD()
        {
            this.DNDoiGVs = new HashSet<DNDoiGV>();
            this.LichGDs = new HashSet<LichGD>();
        }
    
        public int MaPCGD { get; set; }
        public Nullable<int> MaGV { get; set; }
        public Nullable<int> MaHP { get; set; }
        public string Status { get; set; }
        public Nullable<int> MaGV2 { get; set; }
        public Nullable<int> MaGV3 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DNDoiGV> DNDoiGVs { get; set; }
        public virtual GV GV { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichGD> LichGDs { get; set; }
        public virtual LopHP LopHP { get; set; }
    }
}