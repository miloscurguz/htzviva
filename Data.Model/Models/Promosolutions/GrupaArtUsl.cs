using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Data.Model.Models.Promosolutions
{
    public class GrupaArtUsl
    {
    
        [JsonPropertyName("LanguageId")]
        public string LanguageId { get; set; }

        [JsonPropertyName("Id")]
        public string Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("Parent")]
        public string Parent { get; set; }

    }

    public class DataSet
    {
      
        public List<GrupaArtUsl> Table { get; set; }
  
    }
}
