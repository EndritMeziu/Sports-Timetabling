using itc2021.Deserializer.Classes.StructureClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace itc2021.Deserializer.Classes
{
    [XmlRoot(ElementName = "Structure")]
    public class Structure
    {
        [XmlElement(ElementName = "Format")]
        public Format Format { get; set; }
    }
}
