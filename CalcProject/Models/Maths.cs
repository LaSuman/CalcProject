using System.Text.Json.Serialization;

namespace CalculatorProject.Models;

public class Maths
{
    [JsonPropertyName("MyOperation")]
    public Operation? Operation { get; set; }
}

