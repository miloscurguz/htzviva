namespace viva.webstore.Models.Shop
{
    public class ShopSearchCriteriaModel
    {
        public bool ShowNovo { get; set; } = false;
        public bool ShowNajprodavanije { get; set; } = false;
        public int Group_Id { get; set; }
        public int Sub_Group_Id { get; set; }
        public string Keyword { get; set; }
        public int SortMode { get; set; }
        public int? PageNumber { get; set; }
        public int? RecordsByPage { get; set; }
        public string Brand { get; set; }
    }
}
