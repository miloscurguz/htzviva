using System;

namespace viva.webstore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Datum { get; set; }
        public int Placanje { get; set; }
        public string Status { get; set; }
        public string Napomena { get; set; }
        public decimal Iznos { get; set; }
        public decimal Ukupno { get; set; }
        public string Referenca { get; set; }
        public decimal Isporuka { get; set; }
    }
}
