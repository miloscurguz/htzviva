using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Data.Model.Models.Promosolutions
{
    public class Brand_Response
    {
        [JsonPropertyName("LanguageId")]
        public string LanguageId { get; set; }

        [JsonPropertyName("Id")]
        public string Id { get; set; }

        [JsonPropertyName("Image")]
        public string Image { get; set; }

    }
}
