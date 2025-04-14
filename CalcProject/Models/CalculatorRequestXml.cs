using System.Xml.Serialization;

namespace CalculatorProject.Models
{
    [XmlRoot("MyMaths")]
    public class CalculatorRequestXml
    {
        [XmlElement("MyOperation")]
        public MathsXml? Maths { get; set; }
    }
    public class MathsXml
    {
        [XmlElement("MyOperation")]
        public OperationXml? Operation { get; set; }
    }

    public class OperationXml
    {
        [XmlAttribute("ID")]
        public string? ID { get; set; }

        [XmlElement("Value")]
        public List<string>? Value { get; set; }

        [XmlElement("MyOperation")]
        public OperationXml? NestedOperation { get; set; }
    }
}
