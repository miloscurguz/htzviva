using Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace viva.admin.Models.Artikli
{
    public class ArtikalListViewModel
    {
       
        public string Keyword { get; set; }
        public PaginatedList<Artikal> Artikli { get; set; }
        public PaginatedList<Model> Modeli { get; set; }
        public SelectList Grupe { get; set; }
        public SelectList Brendovi { get; set; }
        public string Group_Id { get; set; }
        public string Brand_Id { get; set; }
        public int SortMode { get; set; }
        public string Sort { get; set; }
        public int PageNumber { get; set; }
        public int Total { get; set; }
        public int? RecordByPage { get; set; }
        public string ImagePath { get; set; }
        public bool SamoArtikli { get; set; }
    }
}
