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
    
    public partial class stavkaracuna
    {
        public int idracun { get; set; }
        public int idstavka { get; set; }
        public decimal cenapojm { get; set; }
        public decimal kolicina { get; set; }
        public decimal ukupnacena { get; set; }
        public int idartikal { get; set; }
    
        public virtual racun racun { get; set; }
        public virtual artikal artikal { get; set; }
    }
}
