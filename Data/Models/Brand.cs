using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Data.Models
{
    public partial class Brand
    {
        public int Id { get; set; }
        public string SourceId { get; set; }
        public string Image { get; set; }
        public string Source { get; set; }
    }
}
