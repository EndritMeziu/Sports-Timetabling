using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace itc2021.Deserializer.Classes.ResourcesClasses
{
    [XmlRoot(ElementName = "Leagues")]
    public class Leagues
    {
        [XmlElement (ElementName = "League")]
        public League League { get; set; }
    }

    [XmlRoot(ElementName = "League")]
    public class League
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }
}
