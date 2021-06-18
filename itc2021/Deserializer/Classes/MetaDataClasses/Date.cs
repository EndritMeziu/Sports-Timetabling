using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace itc2021.Deserializer.Classes.MetaDataClasses
{
    [XmlRoot(ElementName = "Date")]
    public class Date
    {
        [XmlAttribute(AttributeName ="year")]
        public string Year { get; set; }
        
        [XmlAttribute(AttributeName = "month")]
        public string Month { get; set; }
    }
}
