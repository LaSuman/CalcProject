using System.Text.Json.Serialization;

namespace CalculatorProject.Models;

public class Operation
{
    [JsonPropertyName("@ID")]
    public string? ID { get; set; }

    public List<string>? Value { get; set; }

    [JsonPropertyName("MyOperation")]
    public List<Operation>? NestedOperation { get; set; } = new();

}
