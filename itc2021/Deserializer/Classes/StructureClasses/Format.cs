using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace itc2021.Deserializer.Classes.StructureClasses
{
    [XmlRoot(ElementName = "Format")]
    public class Format
    {
        [XmlAttribute(AttributeName = "leagueIds")]
        public string LeagueIds { get; set; }

        [XmlElement(ElementName = "numberRoundRobin")]
        public NumberRoundRobin NumberRoundRobin { get; set; }

        [XmlElement(ElementName = "compactness")]
        public Compactness Compactness { get; set; }

        [XmlElement(ElementName= "gameMode")]
        public GameMode GameMode { get; set; }

    }

    [XmlRoot(ElementName = "numberRoundRobin")]
    public class NumberRoundRobin
    {
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "compactness")]
    public class Compactness
    {
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "gameMode")]
    public class GameMode
    {
        [XmlText]
        public string Text { get; set; }
    }
}
