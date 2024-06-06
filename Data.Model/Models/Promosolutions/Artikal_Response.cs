using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Data.Model.Models.Promosolutions
{
    public class Artikal_Response
    {
        [JsonPropertyName("LanguageId")]
        public string LanguageId { get; set; }
        [JsonPropertyName("Id")]
        public string Id { get; set; }
        [JsonPropertyName("ProductIdView")]
        public string ProductIdView { get; set; }
        
        [JsonPropertyName("EAN")]
        public string EAN { get; set; }
        [JsonPropertyName("Price")]
        public string Price { get; set; }
        [JsonPropertyName("Brand")]
        public string Brand { get; set; }
        [JsonPropertyName("Category")]
        public string Category { get; set; }
        [JsonPropertyName("SubCategory")]
        public string SubCategory { get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("Model")]
        public string Model { get; set; }



    }
}
