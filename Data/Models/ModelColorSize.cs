using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Data.Models
{
    public partial class ModelColorSize
    {
        public int Id { get; set; }
        public int? ModelColorId { get; set; }
        public string Size { get; set; }
        public int? ArtikalId { get; set; }

        public virtual Artikal Artikal { get; set; }
        public virtual ModelColor ModelColor { get; set; }
    }
}
