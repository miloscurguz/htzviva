using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Data.Models
{
    public partial class Lacuna2
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Description { get; set; }
        public string Grupa { get; set; }
        public string Brand { get; set; }
        public double? Mpcena { get; set; }
        public double? Pdv { get; set; }
        public double? Popust { get; set; }
        public double? FinalnaCena { get; set; }
        public double? FinalnaCenaBezPdv { get; set; }
        public double? Aktivan { get; set; }
        public bool? ObrisiIzBaze { get; set; }
        public bool? Done { get; set; }
    }
}
