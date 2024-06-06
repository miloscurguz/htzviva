using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Data.Models
{
    public partial class Settings
    {
        public int Id { get; set; }
        public string Magacin { get; set; }
        public string Cenovnik { get; set; }
    }
}
