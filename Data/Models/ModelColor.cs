using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Data.Models
{
    public partial class ModelColor
    {
        public ModelColor()
        {
            ModelColorSize = new HashSet<ModelColorSize>();
        }

        public int Id { get; set; }
        public int? ModelId { get; set; }
        public string Color { get; set; }
        public string HtmlColor { get; set; }

        public virtual Model Model { get; set; }
        public virtual ICollection<ModelColorSize> ModelColorSize { get; set; }
    }
}
