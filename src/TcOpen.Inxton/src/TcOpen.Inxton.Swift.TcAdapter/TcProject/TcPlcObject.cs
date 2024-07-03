using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TcOpen.Inxton.Swift.TcAdapter.TcProject
{
    // using System.Xml.Serialization;
    // XmlSerializer serializer = new XmlSerializer(typeof(TcPlcObject));
    // using (StringReader reader = new StringReader(xml))
    // {
    //    var test = (TcPlcObject)serializer.Deserialize(reader);
    // }

    [XmlRoot(ElementName = "Implementation")]
    public class Implementation
    {
        [XmlElement(ElementName = "ST")]
        public string ST { get; set; }
    }

    [XmlRoot(ElementName = "Method")]
    public class Method
    {
        [XmlElement(ElementName = "Declaration")]
        public string Declaration { get; set; }

        [XmlElement(ElementName = "Implementation")]
        public Implementation Implementation { get; set; }

        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "POU")]
    public class POU
    {
        [XmlElement(ElementName = "Declaration")]
        public string Declaration { get; set; }

        [XmlElement(ElementName = "Implementation")]
        public Implementation Implementation { get; set; }

        [XmlElement(ElementName = "Method")]
        public List<Method> Method { get; set; }

        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "SpecialFunc")]
        public string SpecialFunc { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "TcPlcObject")]
    public class TcPlcObject
    {
        [XmlElement(ElementName = "POU")]
        public POU POU { get; set; }

        [XmlAttribute(AttributeName = "Version")]
        public string Version { get; set; }

        [XmlAttribute(AttributeName = "ProductVersion")]
        public string ProductVersion { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
