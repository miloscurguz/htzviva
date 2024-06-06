using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Data.Model.Models.Lacuna
{
    public class Slika
    {
        [XmlElement(ElementName = "slika")]
        public string slika { get; set; }
    }
}
