using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Data.Models
{
    public partial class Publik
    {
        public double? Id { get; set; }
        public string Naziv { get; set; }
        public string Description { get; set; }
        public string Grupa { get; set; }
        public string Mpcena { get; set; }
        public double? Pdv { get; set; }
        public double? Popust { get; set; }
        public double? FinalnaCena { get; set; }
        public double? FinalnaCenaBezPdv { get; set; }
        public double? Aktivan { get; set; }
        public string ObrisiIzBaze { get; set; }
        public string ZaSveArtikle { get; set; }
    }
}
