using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.Models
{
    public class Shipping
    {
        public int Tip_Kupca { get; set; }
        public string Tip_Kupca_Tekst { get; set; }
        public int Nacin_Isporuke { get; set; }
        public string Nacin_Isporuke_Tekst { get; set; }
        public int Nacin_Placanja { get; set; }
        public string Nacin_Placanja_Tekst { get; set; }
        public string Napomena { get; set; }
        public string Email { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Telefon { get; set; }
        public string Naziv { get; set; }
        public string PIB { get; set; }
        public string Adresa { get; set; }
        public string Grad { get; set; }
        public string PostanskiBroj { get; set; }
    }
}
