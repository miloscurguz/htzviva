using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Data.Model.Models
{
 
        public class OrganizacioneJedinice
        {
            [XmlRoot(ElementName = "Table")]
            public class Table
            {
                [XmlElement(ElementName = "ID")]
                public string ID { get; set; }
                [XmlElement(ElementName = "AnalID")]
                public string AnalID { get; set; }
                [XmlElement(ElementName = "sifra")]
                public string Sifra { get; set; }
                [XmlElement(ElementName = "interninaziv")]
                public string Interninaziv { get; set; }
                [XmlElement(ElementName = "zvanicninaziv")]
                public string Zvanicninaziv { get; set; }
                [XmlElement(ElementName = "postanskibroj")]
                public string Postanskibroj { get; set; }
                [XmlElement(ElementName = "drzava")]
                public string Drzava { get; set; }
                [XmlElement(ElementName = "mesto")]
                public string Mesto { get; set; }
                [XmlElement(ElementName = "adresa")]
                public string Adresa { get; set; }
                [XmlElement(ElementName = "tel")]
                public string Tel { get; set; }
                [XmlElement(ElementName = "fax")]
                public string Fax { get; set; }
                [XmlElement(ElementName = "email")]
                public string Email { get; set; }
                [XmlElement(ElementName = "pib")]
                public string Pib { get; set; }
                [XmlElement(ElementName = "matbroj")]
                public string Matbroj { get; set; }
                [XmlElement(ElementName = "tekuciracun")]
                public string Tekuciracun { get; set; }
                [XmlAttribute(AttributeName = "id", Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1")]
                public string Id { get; set; }
                [XmlAttribute(AttributeName = "rowOrder", Namespace = "urn:schemas-microsoft-com:xml-msdata")]
                public string RowOrder { get; set; }
                [XmlElement(ElementName = "sifranadredjeneorgjed")]
                public string Sifranadredjeneorgjed { get; set; }
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

