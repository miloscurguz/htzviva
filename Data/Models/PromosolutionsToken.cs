using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Data.Models
{
    public partial class PromosolutionsToken
    {
        public int Id { get; set; }
        public string TokenType { get; set; }
        public string AccessToken { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Expires { get; set; }
    }
}
