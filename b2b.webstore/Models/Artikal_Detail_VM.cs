
using Data.Models;
using System.Collections.Generic;
using viva.webstore.Models.File;
using viva.webstore.Models.Product;
using viva.webstore.Models.Slika;
namespace viva.webstore.Models
{
    public class Artikal_Detail_VM
    {

        public Artikal_Detail_VM()
        {
            DodatneSlike = new List<_Slika>();
            Boje = new List<Artikal_Svojstva>();
            Velicine = new List<Artikal_Svojstva>();
            Boje_Moguce = new List<Model_Color>();
            Velicine_Moguce = new List<Model_Size>();
        }
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Webshop_Naziv { get; set; }
        public string Sifra { get; set; }
        public string Barkod { get; set; }
        public int? Artikal_Id { get; set; }
        public string Opis { get; set; }
        public string Opis2 { get; set; }
        public string OpisDetalj { get; set; }
        public string OpisSEO { get; set; }
        public File_Upload_VM FileUpload { get; set; }
        public string MainImage { get; set; }
        public string Image_Source { get; set; }
        public List<_Slika> DodatneSlike { get; set; }
        public string FilePath { get; set; }
        public string VpCena { get; set; }
        public string MpCena { get; set; }
        public double FinalnaCena { get; set; }
        public double FinalnaCenaBezPdv { get; set; }
        public string Popust { get; set; }
        public bool HasPopust { get; set; }
        public string CenaPopust { get; set; }
        public List<Artikal_Svojstva> Boje { get; set; }
        public List<Artikal_Svojstva> Velicine { get; set; }
        public string Brand { get; set; }
        public string Brand_Id { get; set; }
        public string Category { get; set; }
        public string Category_Id { get; set; }
        public string Sub_Category { get; set; }
        public string Sub_Category_Id { get; set; }
        public string Deklaracija { get; set; }
        public List<Model_Color> Boje_Moguce { get; set; }
        public List<Model_Size> Velicine_Moguce { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public bool SingleSize { get; set; }
        public string Size { get; set; }


    }
}
