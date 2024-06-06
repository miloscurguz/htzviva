using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Data.Model.Models.Promosolutions
{
    public class ModelDescrModel
    {
        [JsonPropertyName("DescriptionType")]
        public string DescriptionType { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }
       
    }
}
