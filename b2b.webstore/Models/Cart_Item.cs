using System.Collections.Generic;

namespace viva.webstore.Models
{
    public class Cart_Item
    {
        public long id { get; set; }
        public object properties { get; set; }
        public int quantity { get; set; }
        public long variant_id { get; set; }
        public string key { get; set; }
        public string title { get; set; }
        public double price { get; set; }
        public int original_price { get; set; }
        public int discounted_price { get; set; }
        public int line_price { get; set; }
        public int original_line_price { get; set; }
        public int total_discount { get; set; }
        public List<object> discounts { get; set; }
        public string sku { get; set; }
        public int grams { get; set; }
        public string vendor { get; set; }
        public bool taxable { get; set; }
        public long product_id { get; set; }
        public bool gift_card { get; set; }
        public double final_price { get; set; }
        public int final_line_price { get; set; }
        public string url { get; set; }
        public string image { get; set; }
        public string handle { get; set; }
        public string product_size { get; set; }
        public string product_color { get; set; }
        public bool requires_shipping { get; set; }
        public string product_type { get; set; }
        public string product_title { get; set; }
        public string product_description { get; set; }
        public object variant_title { get; set; }
        public List<string> variant_options { get; set; }
        public List<object> line_level_discount_allocations { get; set; }
    }
}
