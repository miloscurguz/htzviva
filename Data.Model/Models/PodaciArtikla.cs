/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Xml.Schema;

namespace Data.Model.Models
{
    
   

    public class PodaciArtikla
    {
        public class Table
        {
            [XmlElement(ElementName = "artikalID")]
            public string ArtikalID { get; set; }
            [XmlElement(ElementName = "naziv")]
            public string Naziv { get; set; }
            [XmlElement(ElementName = "nazivukasi")]
            public string Nazivukasi { get; set; }
            [XmlElement(ElementName = "barkod")]
            public string Barkod { get; set; }
            [XmlElement(ElementName = "jdm")]
            public string Jdm { get; set; }
            [XmlElement(ElementName = "pdvstopa")]
            public string Pdvstopa { get; set; }
            [XmlElement(ElementName = "uvoznik")]
            public string Uvoznik { get; set; }
            [XmlElement(ElementName = "zemljaporekla")]
            public string Zemljaporekla { get; set; }
            [XmlElement(ElementName = "zemljauvoza")]
            public string Zemljauvoza { get; set; }
            [XmlElement(ElementName = "IDgrupe")]
            public string IDgrupe { get; set; }
            [XmlElement(ElementName = "nazivgrupe")]
            public string Nazivgrupe { get; set; }
            [XmlElement(ElementName = "imaraster")]
            public string Imaraster { get; set; }


            [XmlElement(ElementName = "klasifikacija")]
            public string Klasifikacija { get; set; }
            [XmlElement(ElementName = "standard")]
            public string Standard { get; set; }
            [XmlElement(ElementName = "naakciji")]
            public string Naakciji { get; set; }
            [XmlAttribute(AttributeName = "id", Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1")]
            public string Id { get; set; }
            [XmlAttribute(AttributeName = "rowOrder", Namespace = "urn:schemas-microsoft-com:xml-msdata")]
            public string RowOrder { get; set; }
            [XmlElement(ElementName = "sifragrupe")]
            public string Sifragrupe { get; set; }
            [XmlElement(ElementName = "proizvodjac")]
            public string Proizvodjac { get; set; }
            [XmlElement(ElementName = "sifra")]
            public string Sifra { get; set; }
            [XmlElement(ElementName = "osobina1")]
            public string osobina1 { get; set; }
            [XmlElement(ElementName = "vrednost1")]
            public string vrednost1 { get; set; }
            [XmlElement(ElementName = "osobina2")]
            public string osobina2 { get; set; }
            [XmlElement(ElementName = "vrednost2")]
            public string vrednost2 { get; set; }
            [XmlElement(ElementName = "osobina3")]
            public string osobina3 { get; set; }
            [XmlElement(ElementName = "vrednost3")]
            public string vrednost3 { get; set; }
            [XmlElement(ElementName = "pakovanje")]
            public string pakovanje { get; set; }
            [XmlElement(ElementName = "netomasa")]
            public string netomasa { get; set; }
            [XmlElement(ElementName = "brutomasa")]
            public string brutomasa { get; set; }
            [XmlElement(ElementName = "transportnamasa")]
            public string transportnamasa { get; set; }
            [XmlElement(ElementName = "opis")]
            public string Opis { get; set; }
        }

        [XmlType(AnonymousType = true)]
        [XmlRoot(ElementName = "NewDataSet", Namespace = "", IsNullable = false)]
        public class NewDataSet
        {
            [XmlElement(ElementName = "Table", Form = XmlSchemaForm.Unqualified)]
            public List<Table> Table { get; set; }
            [XmlAttribute(AttributeName = "xmlns")]
            public string Xmlns { get; set; }
        }

    }


}
