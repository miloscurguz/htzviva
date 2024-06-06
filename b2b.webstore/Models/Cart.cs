using System.Collections.Generic;

namespace viva.webstore.Models
{
    public class Cart
    {
        public Cart()
        {
            items = new List<Cart_Item>();
        }
        public string token { get; set; }
        public List<Cart_Item> items { get; set; }
        public double total_price { get; set; }
        public object item_count { get; set; }
        public long current_item_id { get; set; }
        public double current_total_price { get; set; }
    }
}