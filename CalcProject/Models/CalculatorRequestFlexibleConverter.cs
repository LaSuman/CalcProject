﻿using System.Text.Json.Serialization;
using System.Text.Json;

namespace CalculatorProject.Models;

public class CalculatorRequestFlexibleConverter(ILogger<CalculatorRequestFlexibleConverter> logger)
    : JsonConverter<CalculatorRequest>
{
    public override CalculatorRequest? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        var result = new CalculatorRequest();

        // Try to find either "MyMaths" or "Maths"
        if (!root.TryGetProperty("MyMaths", out var mathsElement) &&
            !root.TryGetProperty("Maths", out mathsElement)) 
            return result;

        result.Maths = new Maths();
             
        // Try to find either "MyOperation" or "Operation"
        if (mathsElement.TryGetProperty("MyOperation", out var operationElement) || mathsElement.TryGetProperty("Operation", out operationElement))
        {
            result.Maths.Operation = ParseOperation(operationElement);
        }

        return result;
    }

    private Operation ParseOperation(JsonElement element)
    {
        var op = new Operation();
        try
        {
            if (element.TryGetProperty("@ID", out var id) || element.TryGetProperty("ID", out id))

                op.ID = id.GetString();
        }
        catch (Exception fex)
        {
            logger.LogWarning(fex, "Input format issue: {Message}", fex.Message);
            throw new JsonException($"Input number format: {fex.Message}");
        }

        if (element.TryGetProperty("Value", out var values))
        {
            op.Value = values.EnumerateArray().Select(v => v.GetString() ?? "").ToList();
        }

        if (!element.TryGetProperty("NestedOperation", out var nested) &&
            !element.TryGetProperty("MyOperation", out nested) &&
            !element.TryGetProperty("Operation", out nested)) return op;

        if (nested.ValueKind == JsonValueKind.Array)
        {
            op.NestedOperation = nested.EnumerateArray()
                .Select(ParseOperation)
                .ToList();
        }
        else
        {
            op.NestedOperation = [ParseOperation(nested)];
        }
        return op;
    }

    public override void Write(Utf8JsonWriter writer, CalculatorRequest value, JsonSerializerOptions options)
    {
        throw new NotImplementedException("Writing is not supported.");
    }
}
