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
    
   

    public class CenovnikArtUsl
    {
        public class Table
        {
            [XmlElement(ElementName = "Sifra")]
            public string Sifra { get; set; }
            [XmlElement(ElementName = "Naziv")]
            public string Naziv { get; set; }
            [XmlElement(ElementName = "Grupa")]
            public string Grupa { get; set; }
            [XmlElement(ElementName = "DatumCenovnika")]
            public string DatumCenovnika { get; set; }
            [XmlElement(ElementName = "CenovnikSK")]
            public string CenovnikSK { get; set; }
            [XmlElement(ElementName = "Cena")]
            public string Cena { get; set; }
            [XmlElement(ElementName = "Rabat")]
            public string Rabat { get; set; }
            [XmlElement(ElementName = "Popust")]
            public string Popust { get; set; }

            [XmlElement(ElementName = "Lom")]
            public string Lom { get; set; }
            [XmlElement(ElementName = "ValCen")]
            public string ValCen { get; set; }
            [XmlElement(ElementName = "KojaCena")]
            public string KojaCena { get; set; }
            [XmlElement(ElementName = "Opis")]
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
