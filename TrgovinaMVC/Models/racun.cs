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
    using System.ComponentModel.DataAnnotations;

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
        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime datizdavanja { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> datvalute { get; set; }
        public decimal ukupnacena { get; set; }
        public string nazivkupca { get; set; }
        public string pibkupca { get; set; }
        public string adresakupca { get; set; }
        public string brtelkupca { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<stavkaracuna> stavkaracunas { get; set; }
    }
}
