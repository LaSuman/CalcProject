using CalculatorProject.Models;

namespace CalculatorProject.Services;

public class DivService(ILogger logger) : BaseService
{
    public override double Calculate(CalculatorRequest calculatorRequest)
    {
        logger.LogInformation("Performing division for values: {Values}",
            calculatorRequest.Maths?.Operation?.Value);

        if (calculatorRequest.Maths?.Operation == null)
            throw new NullReferenceException();

        if (calculatorRequest.Maths.Operation.ID != nameof(Operator.Division))
            throw new InvalidOperationException();

        return Calculate(calculatorRequest.Maths.Operation);
    }

    public double Calculate(Operation calculatorRequest)
    {
        if (calculatorRequest.Value == null || calculatorRequest.Value.Count < 2)
            throw new DivideByZeroException("Division requires at least two values.");

        var div = ParseValue(calculatorRequest.Value[0]);

        for (var i = 1; i < calculatorRequest.Value.Count; i++)
        {
            var value = ParseValue(calculatorRequest.Value[i]);

            if (value == 0)
                throw new DivideByZeroException("Cannot divide by zero.");

            div /= value;
        }

        if (calculatorRequest is { Value: { Count: 1 }, NestedOperation: null })
            throw new DivideByZeroException("Cannot divide by zero.");

        // Handle nested calculation, if present.
        if (calculatorRequest.NestedOperation == null) return div;

        div += calculatorRequest.NestedOperation.Sum(nestedOperation => nestedOperation.ID switch
        {
            nameof(Operator.Plus) => new AddService(logger).Calculate(nestedOperation),
            nameof(Operator.Subtraction) => new SubService(logger).Calculate(nestedOperation),
            nameof(Operator.Multiplication) => new MulService(logger).Calculate(nestedOperation),
            nameof(Operator.Division) => new DivService(logger).Calculate(nestedOperation),
            nameof(Operator.Exponential) => new ExpService(logger).Calculate(nestedOperation),
            _ => throw new ArgumentOutOfRangeException { HelpLink = null, HResult = 0, Source = null }
        });

        return div;
    }
}
