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
    
   

    public class GrupaArtUsl
    {
        public class Table
        {
            [XmlElement(ElementName = "ID")]
            public string ID { get; set; }
            [XmlElement(ElementName = "sifra")]
            public string sifra { get; set; }
            [XmlElement(ElementName = "naziv")]
            public string naziv { get; set; }
            [XmlElement(ElementName = "ceonaziv")]
            public string ceonaziv { get; set; }
            [XmlElement(ElementName = "IDnadredjene")]
            public string IDnadredjene { get; set; }
            [XmlElement(ElementName = "sifranadredjene")]
            public string sifranadredjene { get; set; }
            [XmlElement(ElementName = "nazivnadredjene")]
            public string nazivnadredjene { get; set; }
            [XmlElement(ElementName = "nivo")]
            public string nivo { get; set; }
           

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
