using itc2021.Deserializer.Classes.ConstraintClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace itc2021.Deserializer.Classes
{
    [XmlRoot(ElementName = "Constraints")]
    public class Constraints
    {
        [XmlElement(ElementName ="BasicConstraints")]
        public BasicConstraints BasicConstraints { get; set; }

        [XmlElement(ElementName ="CapacityConstraints")]
        public CapacityConstraints CapacityConstraints { get; set; }

        [XmlElement(ElementName ="GameConstraints")]
        public GameConstraints GameConstraints { get; set; }

        [XmlElement(ElementName ="BreakConstraints")]
        public BreakConstraints BreakConstraints { get; set; }

        [XmlElement(ElementName ="FairnessConstraints")]
        public FairnessConstraints FairnessConstraints { get; set; }

        [XmlElement(ElementName ="SeparationConstraints")]
        public SeparationConstraints SeparationConstraints { get; set; }
    }
}
