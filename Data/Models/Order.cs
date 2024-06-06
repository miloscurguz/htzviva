using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Data.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime? Datum { get; set; }
        public int? Placanje { get; set; }
        public string Status { get; set; }
        public string Napomena { get; set; }
        public double? Isporuka { get; set; }
        public double? Iznos { get; set; }
        public double? Ukupno { get; set; }
        public string Referenca { get; set; }
    }
}
