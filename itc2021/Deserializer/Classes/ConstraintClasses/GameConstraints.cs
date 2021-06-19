using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace itc2021.Deserializer.Classes.ConstraintClasses
{
    [XmlRoot(ElementName = "GameConstraints")]
    public class GameConstraints
    {
        [XmlElement(ElementName = "GA1")]
        public List<GA1> GA1 { get; set; }
    }
    
    [XmlRoot(ElementName = "GA1")]
    public class GA1
    {
        [XmlAttribute(AttributeName = "max")]
        public string Max { get; set; }

        [XmlAttribute(AttributeName = "meetings")]
        public string Meetings { get; set; }

        [XmlAttribute(AttributeName = "min")]
        public string Min { get; set; }

        [XmlAttribute(AttributeName = "penalty")]
        public string Penalty { get; set; }
        
        [XmlAttribute(AttributeName = "slots")]
        public string Slots { get; set; }
        
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
    }
    
}
