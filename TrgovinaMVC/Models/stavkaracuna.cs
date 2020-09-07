﻿//------------------------------------------------------------------------------
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

    public partial class stavkaracuna
    {
        public int idracun { get; set; }
        public int idstavka { get; set; }

        [Required(ErrorMessage = "Morate uneti cenu po JM!")]
        [Range(0, 9999999999, ErrorMessage = "Cena mora biti izmedju 0 i 9.999.999.999 din!")]
        public decimal cenapojm { get; set; }

        [Required(ErrorMessage = "Morate uneti količinu!")]
        [Range(0, 9999999999, ErrorMessage = "Količina mora biti izmedju 0 i 9.999.999.999!")]
        public decimal kolicina { get; set; }

        public decimal ukupnacena { get; set; }
        public string nazivartikla { get; set; }
        public string jmartikla { get; set; }

        public virtual racun racun { get; set; }
    }
}
