using itc2021.Deserializer.Classes.MetaDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace itc2021.Deserializer.Classes
{
    [XmlRoot(ElementName = "MetaData")]
    public class MetaData
    {
        [XmlElement(ElementName = "InstanceName")]
        public InstanceName InstanceName { get; set; }

        [XmlElement(ElementName = "DataType")]
        public DataType DataType { get; set; }

        [XmlElement(ElementName = "Contributor")]
        public Contributor Contributor { get; set; }

        [XmlElement(ElementName = "Date")]
        public Date Date { get; set; }
    }
}
