using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace itc2021.Deserializer.Classes.ResourcesClasses
{
    [XmlRoot(ElementName = "Teams")]
    public class Teams
    {
        [XmlElement(ElementName ="team")]
        public List<Team> Team { get; set; }
    }

    [XmlRoot(ElementName = "Team")]
    public class Team
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "league")]
        public string League { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }
}
