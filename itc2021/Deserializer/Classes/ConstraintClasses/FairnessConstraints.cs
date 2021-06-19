using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace itc2021.Deserializer.Classes.ConstraintClasses
{
    [XmlRoot(ElementName = "FairnessConstraints")]
    public class FairnessConstraints
    {
        [XmlElement(ElementName ="FA2")]
        public List<FA2> FA2 { get; set; }
    }
    
    [XmlRoot(ElementName = "FairnessConstraints")]
    public class FA2
    {
        [XmlAttribute(AttributeName ="intp")]
        public string Intp { get; set; }

        [XmlAttribute(AttributeName ="mode")]
        public string Mode { get; set; }
        
        [XmlAttribute(AttributeName ="penalty")]
        public string Penalty { get; set; }

        [XmlAttribute(AttributeName ="slots")]
        public string Slots { get; set; }

        [XmlAttribute(AttributeName ="teams")]
        public string Teams { get; set; }

        [XmlAttribute(AttributeName ="type")]
        public string Type { get; set; }
    }
}
