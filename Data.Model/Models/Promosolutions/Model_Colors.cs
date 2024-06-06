using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.Models.Promosolutions
{
    public class Model_Color
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string HtmlColor { get; set; }
        public List<ModelSize> Sizes { get; set; }
    }
}
