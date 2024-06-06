using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Data.Model.Models.Promosolutions
{
    public class Image_Response
    {
        [JsonPropertyName("No")]
        public int No { get; set; }
        [JsonPropertyName("Image")]
        public string Image { get; set; }
        [JsonPropertyName("ImageWebP")]
        public string ImageWebP { get; set; }
        [JsonPropertyName("ImageGif")]
        public string ImageGif { get; set; }
        [JsonPropertyName("Video")]
        public string Video { get; set; }
    }
}
