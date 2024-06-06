using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Data.Model.Models.Lacuna
{
    [XmlRoot(ElementName = "proizvodi", Namespace = "", IsNullable = false)]
    public class Artikal_Response
    {

        [XmlElement(ElementName = "proizvod", Form = XmlSchemaForm.Unqualified)]
        public List<Proizvod> proizvodi { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    public class Sadrzaj
    {
      
    }

   
    public class Proizvod
    {
        [XmlElement(ElementName = "ID")]
        public string ID { get; set; }
        [XmlElement(ElementName = "naziv")]
        public string Naziv { get; set; }
        [XmlElement(ElementName = "grupa")]
        public string Grupa { get; set; }
        [XmlElement(ElementName = "podgrupa")]
        public string Podgrupa { get; set; }
        [XmlElement(ElementName = "opis")]
        public string Opis { get; set; }
        [XmlElement(ElementName = "brand")]
        public string Brand { get; set; }
        [XmlElement(ElementName = "jedinicneMjere")]
        public string JM { get; set; }
        [XmlElement(ElementName = "barkod")]
        public string Barkod { get; set; }
        [XmlElement(ElementName = "sifra")]
        public string Sifra { get; set; }
        [XmlElement(ElementName = "sifraProizvoda")]
        public string SifraProizvoda { get; set; }
        [XmlElement(ElementName = "slikaProizvoda")]
        public List<Slika> SlikaProizvoda { get; set; }
        [XmlElement(ElementName = "atributi")]
        public Atributi Atributi { get; set; }
        [XmlElement(ElementName = "dostupno")]
        public string Dostupno { get; set; }
        [XmlElement(ElementName = "variant")]
        public string Variant { get; set; }
        [XmlElement(ElementName = "cijena")]
        public string Cena { get; set; }
  
    }

    [XmlRoot(ElementName = "atributi")]
    public class Atributi
    {

        [XmlElement(ElementName = "atribut")]
        public List<Atribut> Atribut { get; set; }
    }


}
