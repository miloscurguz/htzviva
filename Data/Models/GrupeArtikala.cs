using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Data.Models
{
    public partial class GrupeArtikala
    {
        public int Id { get; set; }
        public string SourceId { get; set; }
        public string Naziv { get; set; }
        public string WebshopNaziv { get; set; }
        public bool Aktivan { get; set; }
        public int? Order { get; set; }
        public string Slika { get; set; }
        public bool Naslovna { get; set; }
        public string ParentSourceId { get; set; }
        public int? ParentId { get; set; }
        public string Source { get; set; }
        public bool? AdminList { get; set; }
    }
}
