using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace itc2021.Deserializer.Classes.ConstraintClasses
{
    [XmlRoot(ElementName = "SepartationConstraints")]
    public class SeparationConstraints
    {
        [XmlElement(ElementName ="SE1")]
        public List<SE1> SE1 { get; set; }
    }
    
    [XmlRoot(ElementName = "SepartationConstraints")]
    public class SE1
    {
        [XmlAttribute(AttributeName ="mode1")]
        public string Mode1 { get; set; }

        [XmlAttribute(AttributeName ="min")]
        public string Min { get; set; }

        [XmlAttribute(AttributeName ="penalty")]
        public string Penalty { get; set; }

        [XmlAttribute(AttributeName ="teams")]
        public string Teams { get; set; }
        
        [XmlAttribute(AttributeName ="type")]
        public string Type { get; set; }

    }
}
