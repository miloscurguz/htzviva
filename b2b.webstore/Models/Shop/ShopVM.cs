namespace viva.webstore.Models.Shop
{
    public class ShopVM
    {
        public PaginatedList<Data.Models.Model> Artikli { get; set; }
        public int SortMode { get; set; }
        public string Sort { get; set; }
        public int PageNumber { get; set; }
        public int Group_Id { get; set; }
        public int Sub_Group_Id { get; set; }
        public string Keyword { get; set; }
        public int Total { get; set; }
        public int? RecordByPage { get; set; }
        public string ImagePath { get; set; }
        public string Brand { get; set; }
    }
}
