using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Data.Models
{
    public partial class Adresa
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string AdresaPl { get; set; }
        public string Telefon1Pl { get; set; }
        public string Telefon2Pl { get; set; }
        public string GradPl { get; set; }
        public string PbrojPl { get; set; }
        public string AdresaPr { get; set; }
        public string Telefon2Pr { get; set; }
        public string Telefon1Pr { get; set; }
        public string GradPr { get; set; }
        public string PbrojPr { get; set; }
    }
}
