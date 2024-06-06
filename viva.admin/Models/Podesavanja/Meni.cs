namespace viva.admin.Models.Podesavanja
{
    public class Meni
    {
        public string Id { get; set; }
        public string Naziv { get; set; }
        public int? GrupaId { get; set; }
        public int? Parent { get; set; }
    }
}
