using CalculatorProject.Models;

namespace CalculatorProject.Services;

internal class ExpService(ILogger logger) : BaseService()
{
    public override double Calculate(CalculatorRequest calculatorRequest)
    {
        logger.LogInformation("Performing exponential for values: {Values}", calculatorRequest.Maths?.Operation?.Value);

        if (calculatorRequest?.Maths?.Operation == null)
            throw new ArgumentNullException(nameof(calculatorRequest),
                "CalculatorRequest, Maths, or Operation cannot be null.");

        return Calculate(calculatorRequest.Maths.Operation);
    }

    internal double Calculate(Operation? op)
    {
        if (op?.Value is not { Count: 2 })
            throw new InvalidOperationException(
                "Exponential operation requires exactly two operands: base and exponent.");

        var baseStr = op.Value[0].Trim().ToLower();
        var exponentStr = op.Value[1].Trim().ToLower();

        var baseNum = baseStr == "e" ? Math.E : double.Parse(baseStr);
        var exponent = exponentStr == "e" ? Math.E : double.Parse(exponentStr);

        return Math.Pow(baseNum, exponent);
    }
}
