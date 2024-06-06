using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Data.Models
{
    public partial class Artikal
    {
        public Artikal()
        {
            ArtikalDetalj = new HashSet<ArtikalDetalj>();
            ArtikalSlike = new HashSet<ArtikalSlike>();
            ArtikalSvojstva = new HashSet<ArtikalSvojstva>();
            CartItem = new HashSet<CartItem>();
            ModelColorSize = new HashSet<ModelColorSize>();
        }

        public int Id { get; set; }
        public string Sifra { get; set; }
        public string WebshopNaziv { get; set; }
        public string Naziv { get; set; }
        public string Barkod { get; set; }
        public string Jm { get; set; }
        public string Slika { get; set; }
        public decimal? Vpcena { get; set; }
        public decimal? Mpcena { get; set; }
        public double? FinalnaCena { get; set; }
        public int? Pdv { get; set; }
        public double? FinalnaCenaPdv { get; set; }
        public double? Popust { get; set; }
        public bool? Novo { get; set; }
        public bool? Najprodavaniji { get; set; }
        public string SourceId { get; set; }
        public bool? Aktivan { get; set; }
        public string SourceSifra { get; set; }
        public bool? NaStanju { get; set; }
        public string Source { get; set; }
        public string Brand { get; set; }
        public string SlikaSource { get; set; }
        public int? ModelId { get; set; }
        public bool? IsModel { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Temp { get; set; }

        public virtual ICollection<ArtikalDetalj> ArtikalDetalj { get; set; }
        public virtual ICollection<ArtikalSlike> ArtikalSlike { get; set; }
        public virtual ICollection<ArtikalSvojstva> ArtikalSvojstva { get; set; }
        public virtual ICollection<CartItem> CartItem { get; set; }
        public virtual ICollection<ModelColorSize> ModelColorSize { get; set; }
    }
}
