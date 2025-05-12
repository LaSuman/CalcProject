using CalculatorProject.Models;

namespace CalculatorProject.Services;
public class MulService(ILogger logger) : BaseService()
{
    public override double Calculate(CalculatorRequest calculatorRequest)
    {
        logger.LogInformation("Performing multiplication for values: {Values}", calculatorRequest.Maths?.Operation?.Value);

        if (calculatorRequest.Maths?.Operation == null)
            throw new NullReferenceException();

        if (calculatorRequest.Maths.Operation.ID != nameof(Operator.Multiplication))
            throw new InvalidOperationException();

        return Calculate(calculatorRequest.Maths.Operation);
    }

    public double Calculate(Operation calculatorRequest)
    {
        double mul = 1;

        // Handle direct values (including "e")
        if (calculatorRequest.Value != null)
        {
            mul = calculatorRequest.Value.Aggregate(mul, (current, value) => current * ParseValue(value));
        }

        // Handle nested operations
        if (calculatorRequest.NestedOperation == null) return mul;

        return calculatorRequest.NestedOperation.Select(nestedOperation => nestedOperation.ID switch
            {
                nameof(Operator.Plus) => new AddService(logger).Calculate(nestedOperation),
                nameof(Operator.Subtraction) => new SubService(logger).Calculate(nestedOperation),
                nameof(Operator.Multiplication) => new MulService(logger).Calculate(nestedOperation),
                nameof(Operator.Division) => new DivService(logger).Calculate(nestedOperation),
                nameof(Operator.Exponential) => new ExpService(logger).Calculate(nestedOperation),
                _ => throw new ArgumentOutOfRangeException($"Unsupported operator: {nestedOperation.ID}")
            })
            .Aggregate(mul, (current, nestedResult) => current * nestedResult);
    }
}
