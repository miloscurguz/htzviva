using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Data.Model.Models.Promosolutions
{
    public class Size_Response
    {
        [JsonPropertyName("Id")]
        public string Id { get; set; }
        [JsonPropertyName("Category")]
        public string Category { get; set; }
    }
}
