using itc2021.Deserializer.Classes.ResourcesClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace itc2021.Deserializer.Classes
{
    [XmlRoot(ElementName = "Resources")]
    public class Resources
    {
        [XmlElement(ElementName = "Leagues")]
        public Leagues Leagues { get; set; }

        [XmlElement(ElementName = "Teams")]
        public Teams Teams { get; set; }

        [XmlElement(ElementName = "Slots")]
        public Slots Slots { get; set; }
    }
}
