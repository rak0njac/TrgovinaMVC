//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrgovinaMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class racun
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public racun()
        {
            this.stavkaracunas = new HashSet<stavkaracuna>();
        }
    
        public int idracun { get; set; }
        public int brracuna { get; set; }
        public string tipracuna { get; set; }
        public Nullable<int> godina { get; set; }
        public System.DateTime datizdavanja { get; set; }
        public Nullable<System.DateTime> datvalute { get; set; }
        public decimal ukupnacena { get; set; }
        public Nullable<bool> db_hidden { get; set; }
        public string pib { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<stavkaracuna> stavkaracunas { get; set; }
        public virtual kupac kupac { get; set; }
    }
}
