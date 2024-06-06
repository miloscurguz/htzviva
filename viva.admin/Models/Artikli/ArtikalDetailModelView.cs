using viva.admin.Models.Files;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace viva.admin.Models.Artikli
{
    public class ArtikalDetailModelView
    {
        public ArtikalDetailModelView() {
            DodatneSlike = new List<Slika>();
            Svojstva = new List<Artikal_Svojstva>();
        }
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Webshop_Naziv { get; set; }
        public int? Artikal_Id {get;set;}
        public int? Model_Id { get; set; }
        public int? Model_Id_Old { get; set; }
        public string Model_Naziv { get; set; }
        public string Opis { get; set; }
        public string Opis2 { get; set; }
        public bool OpisOverride { get; set; }
        public string OpisSEO { get; set; }
        public FileUploadViewModel FileUpload { get; set; }
        public string MainImage { get; set; }
        public string Image_Source { get; set; }
        public List<Slika> DodatneSlike { get; set; }
        public string FilePath { get; set; }
        public string VpCena { get; set; }
        public string MpCena { get;set; }
        public string Popust { get; set; }
        public int PDV { get; set; }
        public string FinalnaCena { get; set; }
        public string Deklaracija { get; set; }
        public List<Artikal_Svojstva> Svojstva { get; set; }
        public string Boja { get; set; }
        public string Velicina { get; set; }
        public string Source { get; set; }
   
    }
}
