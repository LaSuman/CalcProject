using System.Text.Json.Serialization;

namespace CalculatorProject.Models
{
    public class CalculatorRequest
    {
        [JsonPropertyName("MyMaths")]
        public Maths? Maths { get; set; }
    }
}
