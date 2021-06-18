using System;
using System.IO;
using System.Xml.Serialization;

namespace itc2021.Deserializer
{
    public class XmlDeserializer
    {
        public T DeserializeXml<T>(string input)
        {
            var serializer = new XmlSerializer(typeof(T));
            var fileStream = new FileStream(input, FileMode.Open);

            return (T)serializer.Deserialize(fileStream);
        }
    }
}
