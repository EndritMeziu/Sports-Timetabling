using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace itc2021.Deserializer.Classes.MetaDataClasses
{
    [XmlRoot(ElementName = "InstanceName")]
    public class InstanceName
    {
        [XmlText]
        public string Text { get; set; }
    }
}
