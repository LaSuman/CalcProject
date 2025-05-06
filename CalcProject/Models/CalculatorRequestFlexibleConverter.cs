using System.Text.Json.Serialization;
using System.Text.Json;

namespace CalculatorProject.Models;

public class CalculatorRequestFlexibleConverter : JsonConverter<CalculatorRequest>
{
    public override CalculatorRequest? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        var result = new CalculatorRequest();

        // Try to find either "MyMaths" or "Maths"
        JsonElement mathsElement;
        if (root.TryGetProperty("MyMaths", out mathsElement) || root.TryGetProperty("Maths", out mathsElement))
        {
            result.Maths = new Maths();

            // Try to find either "MyOperation" or "Operation"
            if (mathsElement.TryGetProperty("MyOperation", out var operationElement) || mathsElement.TryGetProperty("Operation", out operationElement))
            {
                result.Maths.Operation = ParseOperation(operationElement);
            }
        }

        return result;
    }

    private Operation ParseOperation(JsonElement element)
    {
        var op = new Operation();

        if (element.TryGetProperty("@ID", out var id))
            op.ID = id.GetString();

        if (element.TryGetProperty("Value", out var values))
        {
            op.Value = values.EnumerateArray().Select(v => v.GetString() ?? "").ToList();
        }

        if (element.TryGetProperty("MyOperation", out var nested) || element.TryGetProperty("Operation", out nested))
        {
            op.NestedOperation = ParseOperation(nested);
        }

        return op;
    }

    public override void Write(Utf8JsonWriter writer, CalculatorRequest value, JsonSerializerOptions options)
    {
        throw new NotImplementedException("Writing is not supported.");
    }
}
