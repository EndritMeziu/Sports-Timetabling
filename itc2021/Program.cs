using itc2021.Deserializer;
using itc2021.Deserializer.Classes;
using System;
using System.IO;

namespace itc2021
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDeserializer deserializer = new XmlDeserializer();
            var obj = deserializer.DeserializeXml<Instance>(@"C:\Users\USER\Desktop\AI Project\SportsTimeTabling\Test Instances EM\ITC2021_Test4.xml");
            int x = 2;
        }
    }
}
