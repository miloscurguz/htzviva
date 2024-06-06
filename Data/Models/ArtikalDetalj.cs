using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Data.Models
{
    public partial class ArtikalDetalj
    {
        public int Id { get; set; }
        public int ArtikalId { get; set; }
        public string Opis { get; set; }
        public string WebshopNaziv { get; set; }
        public bool OpisOverride { get; set; }
        public string Opis2 { get; set; }
        public string OpisSeo { get; set; }
        public string Deklaracija { get; set; }

        public virtual Artikal Artikal { get; set; }
    }
}
