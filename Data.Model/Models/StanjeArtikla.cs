using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Data.Model.Models
{
 
        public class StanjeArtikla
        {
            [XmlRoot(ElementName = "Table")]
            public class Table
            {
                [XmlElement(ElementName = "sifra")]
                public string sifra { get; set; }
                [XmlElement(ElementName = "naziv")]
                public string naziv { get; set; }
                [XmlElement(ElementName = "jdm")]
                public string jdm { get; set; }
                [XmlElement(ElementName = "stanje")]
                public string stanje { get; set; }
                [XmlElement(ElementName = "magacin")]
                public string magacin { get; set; }
                [XmlElement(ElementName = "rezervisano")]
                public string rezervisano { get; set; }
                [XmlElement(ElementName = "pozajmice")]
                public string pozajmice { get; set; }
                [XmlElement(ElementName = "grupa")]
                public string grupa { get; set; }
               
            }

            [XmlRoot(ElementName = "NewDataSet")]
            public class NewDataSet
            {
                [XmlElement(ElementName = "Table", Form = XmlSchemaForm.Unqualified)]
                public List<Table> Table { get; set; }
                [XmlAttribute(AttributeName = "xmlns")]
                public string Xmlns { get; set; }
            }

        }
    }

