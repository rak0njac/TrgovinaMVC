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

    public partial class kupac
    {
        [Required(ErrorMessage = "Morate uneti PIB!")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "PIB mora biti tačno 9 cifara!")]
        public string pib { get; set; }

        [Required(ErrorMessage = "Morate uneti naziv kupca!")]
        public string naziv { get; set; }

        [Required(ErrorMessage = "Morate uneti adresu kupca!")]
        public string adresa { get; set; }

        [Required(ErrorMessage = "Morate uneti broj telefona!")]
        public string brtel { get; set; }

        public int idkupac { get; set; }
    }
}
