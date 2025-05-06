using System.Xml.Serialization;

namespace CalculatorProject.Models
{
    [XmlRoot("MyMaths")]
    public class CalculatorRequestXml
    {
        [XmlElement("MyOperation")]
        public OperationXml? Operation { get; set; }
    }

    [XmlRoot("MyOperation")]

    public class OperationXml
    {
        [XmlAttribute("ID")]
        public string ID { get; set; }

        [XmlElement("Value")]
        public List<string>? Value { get; set; }

        [XmlElement("MyOperation")]
        public List<OperationXml>? NestedOperation { get; set; } = new();
    }
}