using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Data.Models
{
    public partial class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ArtikalId { get; set; }
        public double Kolicina { get; set; }
        public double Cena { get; set; }

        public virtual Artikal Artikal { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
