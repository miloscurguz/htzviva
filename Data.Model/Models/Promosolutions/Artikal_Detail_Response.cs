using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Data.Model.Models.Promosolutions
{
    public class Artikal_Detail_Response
    {
        [JsonPropertyName("LanguageId")]
        public string LanguageId { get; set; }
        [JsonPropertyName("Id")]
        public string Id { get; set; }
        [JsonPropertyName("Description")]
        public string Description { get; set; }
        [JsonPropertyName("Description2")]
        public string Description2 { get; set; }
        [JsonPropertyName("ProductIdView")]
        public string ProductIdView { get; set; }
        [JsonPropertyName("EAN")]
        public string EAN { get; set; }
        [JsonPropertyName("Price")]
        public string Price { get; set; }
        [JsonPropertyName("Brand")]
        public Brand_Response Brand { get; set; }
        [JsonPropertyName("Category")]
        public Category_Response Category { get; set; }

        [JsonPropertyName("SubCategory")]
        public Category_Response SubCategory { get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("Model")]
        public Model_Reponse Model { get; set; }
        [JsonPropertyName("Images")]
        public List<Image_Response> Images { get; set; }
        [JsonPropertyName("Color")]
        public Color_Response Color { get; set; }
        [JsonPropertyName("ProductSize")]
        public Size_Response Size { get; set; }
        [JsonPropertyName("Stocks")]
        public List<Stock_Response> Stocks { get; set; }
        [JsonPropertyName("ExtDescr")]
        public List<ModelDescrModel> ExtDescr { get; set; }
        
    }
}
