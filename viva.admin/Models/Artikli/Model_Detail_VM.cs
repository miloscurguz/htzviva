using viva.admin.Models.Files;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace viva.admin.Models.Artikli
{
    public class Model_Detail_VM
    {
        public Model_Detail_VM() {
            DodatneSlike = new List<Slika>();
            Artikli = new List<Artikal>();
            Grupe = new List<SelectListItem>();
            Pod_Grupe = new List<SelectListItem>();
        }
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Webshop_Naziv { get; set; }
        public int? Model_Id{get;set;}
        public string Opis { get; set; }
        public string Opis2 { get; set; }
        public string OpisDetalj { get; set; }
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
        public string FinalnaCenaBezPdv { get; set; }
        public string Deklaracija { get; set; }
        public List<Artikal> Artikli { get; set; }
        public string Tag { get; set; }
        public string Source { get; set; }
        public List<SelectListItem> Grupe { get; set; }
        public string Grupa { get; set; }
        public List<SelectListItem> Pod_Grupe { get; set; }
        public string Pod_Grupa { get; set; }

    }
}
