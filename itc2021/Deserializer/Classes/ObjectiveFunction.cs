using itc2021.Deserializer.Classes.ObjectiveFunctionClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace itc2021.Deserializer.Classes
{
    [XmlRoot(ElementName = "ObjectiveFunction")]
    public class ObjectiveFunction
    {
        [XmlElement(ElementName = "Objective")]
        public Objective Objective { get; set; } 
    }
}
