using Microsoft.AspNetCore.Mvc.Rendering;

namespace viva.admin.Models.Artikli
{
    public class ShopSearchCriteriaModel
    {
        public string Group_Id { get; set; }
        public string Brand_Id { get; set; }
        public string Keyword { get; set; }
        public int SortMode { get; set; }
        public int? PageNumber { get; set; }
        public int? RecordsByPage { get; set; }
        public bool SamoArtikle { get; set; }
        public string Brand { get; set; }
      
    }
}
