using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Data.Models
{
    public partial class ArtikalSlike
    {
        public int Id { get; set; }
        public int? ArtikalId { get; set; }
        public string Slika { get; set; }
        public string Source { get; set; }
        public bool? Main { get; set; }

        public virtual Artikal Artikal { get; set; }
    }
}
