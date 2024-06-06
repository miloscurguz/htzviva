using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.Models
{
    public  class StavkaDokumenta
    {
        public int id { get; set; }
        public string KljucDok { get; set; }
        public string Magacin { get; set; }
        public string ArtUsl { get; set; }
        public string SifArtUsl { get; set; }
        public string NazArtUsl { get; set; }
        public decimal Kolicina { get; set; }
        public string NabCena { get; set; }
        public string VPCena { get; set; }
        public string MPCena { get; set; }
        public string Popust { get; set; }
        public string PoUlazu { get; set; }
        public string Napomena { get; set; }
        public decimal Masa { get; set; }
        public bool Raster { get; set; }
        public string Barkod { get; set; }
        public string Osobina1 { get; set; }
        public string Vrednost1 { get; set; }
        public string Osobina2 { get; set; }
        public string Vrednost2 { get; set; }
        public string Osobina3 { get; set; }
        public string Vrednost3 { get; set; }
        public decimal Zapremljeno { get; set; }
        public string IdStavke { get; set; }
        public bool MultyBarkod { get; set; }
        public string Pozicija { get; set; }
        public decimal UnetaKolicina { get; set; }
        public bool RasteriSkinutiSaServisa { get; set; }
        public bool RasteriSinhroizovani { get; set; }
        public int rb { get; set; }
        public decimal kolicinaz { get; set; }
        public decimal kolicinar { get; set; }
        public decimal kolicinac { get; set; }
        public decimal kolicinab { get; set; }
        public decimal kolicinaf { get; set; }
        public decimal porucenakol { get; set; }
        public string Realizovano { get; set; }
        public decimal Razlika { get; set; }
        public string tarifa { get; set; }
        public string kb { get; set; }
        public string rabatk { get; set; }
        public string popustk { get; set; }
        public string lomk { get; set; }
        public string rabatd { get; set; }
        public string popustd { get; set; }
        public string lomd { get; set; }
    }
}
