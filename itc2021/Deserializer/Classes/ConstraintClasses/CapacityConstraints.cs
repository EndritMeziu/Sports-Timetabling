using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace itc2021.Deserializer.Classes.ConstraintClasses
{
    [XmlRoot(ElementName = "CapacityConstraints")]
    public class CapacityConstraints
    {
        [XmlElement(ElementName ="CA1")]
        public List<CA1> CA1 { get; set; }
        
        [XmlElement(ElementName ="CA2")]
        public List<CA2> CA2 { get; set; }

        [XmlElement(ElementName = "CA3")]
        public List<CA3> CA3 { get; set; }

        [XmlElement(ElementName = "CA4")]
        public List<CA4> CA4 { get; set; }
    }

    [XmlRoot(ElementName = "CA1")]
    public class CA1
    {
        [XmlAttribute(AttributeName ="max")]
        public string Max { get; set; }

        [XmlAttribute(AttributeName ="min")]
        public string Min { get; set; }

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


    [XmlRoot(ElementName = "CA2")]
    public class CA2
    {
        [XmlAttribute(AttributeName ="max")]
        public string Max { get; set; }

        [XmlAttribute(AttributeName ="min")]
        public string Min { get; set; }
        
        [XmlAttribute(AttributeName ="mode1")]
        public string Mode1 { get; set; }
        
        [XmlAttribute(AttributeName ="mode2")]
        public string Mode2 { get; set; }
        
        
        [XmlAttribute(AttributeName ="penalty")]
        public string Penalty { get; set; }
        
        [XmlAttribute(AttributeName ="slots")]
        public string Slots { get; set; }
       
        [XmlAttribute(AttributeName ="teams1")]
        public string Teams1 { get; set; }
        
        [XmlAttribute(AttributeName ="teams2")]
        public string Teams2 { get; set; }
        
        [XmlAttribute(AttributeName ="type")]
        public string Type { get; set; }
    }

    [XmlRoot(ElementName = "CA3")]
    public class CA3
    {
        [XmlAttribute(AttributeName ="intp")]
        public string Intp { get; set; }

        [XmlAttribute(AttributeName ="max")]
        public string Max { get; set; }

        [XmlAttribute(AttributeName ="min")]
        public string Min { get; set; }

        [XmlAttribute(AttributeName ="mode1")]
        public string Mode1 { get; set; }

        [XmlAttribute(AttributeName ="mode2")]
        public string Mode2 { get; set; }
        
        [XmlAttribute(AttributeName ="penalty")]
        public string Penalty { get; set; }
        
        [XmlAttribute(AttributeName ="teams1")]
        public string Teams1 { get; set; }
        
        [XmlAttribute(AttributeName ="teams2")]
        public string Teams2 { get; set; }
        
        [XmlAttribute(AttributeName ="type")]
        public string Type { get; set; }
    }

    [XmlRoot(ElementName = "CA4")]
    public class CA4
    {
        [XmlAttribute(AttributeName = "max")]
        public string Max { get; set; }

        [XmlAttribute(AttributeName = "min")]
        public string Min { get; set; }

        [XmlAttribute(AttributeName = "mode1")]
        public string Mode1 { get; set; }

        [XmlAttribute(AttributeName = "mode2")]
        public string Mode2 { get; set; }

        [XmlAttribute(AttributeName = "penalty")]
        public string Penalty { get; set; }

        [XmlAttribute(AttributeName ="slots")]
        public string Slots { get; set; }

        [XmlAttribute(AttributeName = "teams1")]
        public string Teams1 { get; set; }

        [XmlAttribute(AttributeName = "teams2")]
        public string Teams2 { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
    }
}
