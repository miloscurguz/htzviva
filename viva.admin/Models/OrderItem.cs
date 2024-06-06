using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace viva.admin.Models
{
    public partial class OrderItem
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? ArtikalId { get; set; }
        public double? Kolicina { get; set; }
        public double? Cena { get; set; }
        public int? CartItemId { get; set; }
    }
}
