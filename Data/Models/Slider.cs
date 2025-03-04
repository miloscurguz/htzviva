﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Data.Models
{
    public partial class Slider
    {
        public int Id { get; set; }
        public string Slika { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public int? Order { get; set; }
    }
}
