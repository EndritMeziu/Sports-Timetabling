using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace itc2021.Deserializer.Classes.ResourcesClasses
{
    [XmlRoot(ElementName = "Slots")]
    public class Slots
    {
        [XmlElement(ElementName ="slot")]
        public List<Slot> Slot { get; set; }
    }
    
    [XmlRoot(ElementName = "slot")]
    public class Slot
    {
        [XmlAttribute(AttributeName ="id")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName ="name")]
        public string Name { get; set; }
    }
}