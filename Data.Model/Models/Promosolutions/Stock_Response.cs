using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Data.Model.Models.Promosolutions
{
    public class Stock_Response
    {
        [JsonPropertyName("Warehouse")]
        public string Warehouse { get; set; }
        [JsonPropertyName("Qty")]
        public string Qty { get; set; }
    }
}
