using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace itc2021.Deserializer.Classes.ConstraintClasses
{
    [XmlRoot(ElementName = "BreakConstraints")]
    public class BreakConstraints
    {
        [XmlElement(ElementName ="BR1")]
        public List<BR1> BR1 { get; set; }

        [XmlElement(ElementName ="BR2")]
        public List<BR2> BR2 { get; set; }
    }

    [XmlRoot(ElementName = "BreakConstraints")]
    public class BR1
    {
        [XmlAttribute(AttributeName ="intp")]
        public string Intp { get; set; }
        
        [XmlAttribute(AttributeName ="mode1")]
        public string Mode1 { get; set; }
        
        [XmlAttribute(AttributeName ="mode2")]
        public string Mode2 { get; set; }
        
        [XmlAttribute(AttributeName ="penalty")]
        public string Penalty { get; set; }
        
        [XmlAttribute(AttributeName ="slots")]
        public string Slots { get; set; }
        
        [XmlAttribute(AttributeName ="teams")]
        public string Teams { get; set; }

        [XmlAttribute(AttributeName ="type")]
        public string Type { get; set; }
    }

    [XmlRoot(ElementName = "BreakConstraints")]
    public class BR2
    {
        [XmlAttribute(AttributeName ="intp")]
        public string Intp { get; set; }
        
        [XmlAttribute(AttributeName ="homeMode")]
        public string HomeMode { get; set; }
        
        [XmlAttribute(AttributeName ="mode2")]
        public string Mode2 { get; set; }

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
