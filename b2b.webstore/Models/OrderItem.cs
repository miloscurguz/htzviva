namespace viva.webstore.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? ArtikalId { get; set; }
        public string ArtikalName { get; set; }
        public double? Kolicina { get; set; }
        public double? Cena { get; set; }
        public int? CartItemId { get; set; }
        public string Magacin { get; set; }
    }
}
