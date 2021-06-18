using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace itc2021.Deserializer.Classes
{

    [XmlRoot(ElementName = "Instance")]
    public class Instance
    {
        [XmlElement(ElementName = "MetaData")]
        public MetaData MetaData { get; set; }

        [XmlElement(ElementName = "Structure")]
        public Structure Structure { get; set; }

        [XmlElement(ElementName = "ObjectiveFunction")]
        public ObjectiveFunction ObjectiveFunction { get; set; }
        
        [XmlElement(ElementName = "Resources")]
        public Resources Resources { get; set; }

        [XmlElement(ElementName = "Constraints")]
        public Constraints Constraints { get; set; }
    }
}
