using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Data.Models
{
    public partial class Model
    {
        public Model()
        {
            ModelColor = new HashSet<ModelColor>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Slika { get; set; }
        public string SlikaSource { get; set; }
        public int? Grupa1 { get; set; }
        public int? Grupa2 { get; set; }
        public int? Grupa3 { get; set; }
        public string SourceId { get; set; }
        public string WebshopNaziv { get; set; }
        public decimal? Vpcena { get; set; }
        public decimal? Mpcena { get; set; }
        public double? FinalnaCena { get; set; }
        public int? Pdv { get; set; }
        public double? FinalnaCenaPdv { get; set; }
        public string Video { get; set; }
        public string Source { get; set; }
        public string Brand { get; set; }
        public bool Aktivan { get; set; }
        public bool? Novo { get; set; }
        public bool? Najprodavaniji { get; set; }
        public string Description { get; set; }
        public string Description2 { get; set; }
        public string Tag { get; set; }
        public double? Popust { get; set; }
        public string ZemljaPorekla { get; set; }

        public virtual ICollection<ModelColor> ModelColor { get; set; }
    }
}
