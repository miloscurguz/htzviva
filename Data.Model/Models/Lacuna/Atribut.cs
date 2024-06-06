using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Data.Model.Models.Lacuna
{
    [XmlRoot(ElementName = "atribut")]
    public class Atribut
    {
        [XmlElement(ElementName = "naziv")]
        public string Naziv { get; set; }
        [XmlElement(ElementName = "vrijednost")]
        public string Vrednost { get; set; }
    }
}
