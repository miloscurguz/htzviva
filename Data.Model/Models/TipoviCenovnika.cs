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
    
   

    public class TipoviCenovnika
    {
        public class Table
        {
            [XmlElement(ElementName = "ID")]
            public string ID { get; set; }
            [XmlElement(ElementName = "Sifra")]
            public string Sifra { get; set; }
            [XmlElement(ElementName = "Naziv")]
            public string Naziv { get; set; }
            [XmlElement(ElementName = "IDGrupeArtUsl")]
            public string IDGrupeArtUsl { get; set; }
            [XmlElement(ElementName = "BaznaValuta")]
            public string BaznaValuta { get; set; }
            [XmlElement(ElementName = "UvekBaznaValuta")]
            public string UvekBaznaValuta { get; set; }
            [XmlElement(ElementName = "KursBazneValute")]
            public string KursBazneValute { get; set; }
            [XmlElement(ElementName = "PopunitiCenu")]
            public string PopunitiCenu { get; set; }
           

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
