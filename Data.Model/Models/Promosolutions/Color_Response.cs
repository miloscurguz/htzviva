using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Data.Model.Models.Promosolutions
{
    public class Color_Response
    {

        [JsonPropertyName("LanguageId")]
        public string LanguageId { get; set; }
        [JsonPropertyName("Id")]
        public string Id { get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("HtmlColor")]
        public string HtmlColor { get; set; }
   
    }
}
