using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Data.Models
{
    public partial class Meni
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int? GrupaId { get; set; }
        public int? Parent { get; set; }
    }
}
