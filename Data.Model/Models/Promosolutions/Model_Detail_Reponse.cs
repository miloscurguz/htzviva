using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Data.Model.Models.Promosolutions
{
    public class Model_Detail_Reponse
    {
        [JsonPropertyName("Id")]
        public string Id { get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("Image")]
        public string Image { get; set; }
        [JsonPropertyName("ImageWebP")]
        public string ImageWebP { get; set; }
        [JsonPropertyName("Description")]
        public string Description { get; set; }
        [JsonPropertyName("Description2")]
        public string Description2 { get; set; }

        [JsonPropertyName("Group1")]
        public string Group1 { get; set; }
        [JsonPropertyName("Group2")]
        public string Group2 { get; set; }
        [JsonPropertyName("GroupWeb1")]
        public string GroupWeb1 { get; set; }
        [JsonPropertyName("GroupWeb2")]
        public string GroupWeb2 { get; set; }
        [JsonPropertyName("GroupWeb3")]
        public string GroupWeb3 { get; set; }
        [JsonPropertyName("Video")]
        public string Video { get; set; }
        [JsonPropertyName("Colors")]
        public List<Model_Color> Colors { get; set; }
        [JsonPropertyName("ExtDescr")]
        public List<ModelDescrModel> ExtDescr { get; set; }
    }
}
