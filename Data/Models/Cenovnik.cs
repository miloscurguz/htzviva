using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Data.Models
{
    public partial class Cenovnik
    {
        public int Id { get; set; }
        public string TipCenovnikaId { get; set; }
        public string SifraArtikla { get; set; }
        public DateTime? Datum { get; set; }
        public decimal? Cena { get; set; }
        public decimal? Rabat { get; set; }
        public decimal? Popust { get; set; }
        public decimal? Lom { get; set; }
    }
}
