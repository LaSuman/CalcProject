using CalculatorProject.Controllers;
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
        double mul = Calculate(calculatorRequest.Maths.Operation);
        return mul;
    }


    public double Calculate(Operation calculatorRequest)
    {
        var mul = double.Parse(calculatorRequest.Value[0]);

        for (var i = 1; i < calculatorRequest.Value.Count; i++)
        {

            var value = double.Parse(calculatorRequest.Value[i]);
            mul *= value;
        }
        if (calculatorRequest.Value.Count == 1 && calculatorRequest.NestedOperation == null)
            mul = 0;

        // Handle nested calculation, if present.
        if (calculatorRequest.NestedOperation == null) return mul;

        foreach (var nestedOperation in calculatorRequest.NestedOperation)
        {
            var nestedResult = nestedOperation.ID switch
            {
                nameof(Operator.Plus) => new AddService(logger).Calculate(nestedOperation),
                nameof(Operator.Subtraction) => new SubService(logger).Calculate(nestedOperation),
                nameof(Operator.Multiplication) => new MulService(logger).Calculate(nestedOperation),
                nameof(Operator.Division) => new DivService(logger).Calculate(nestedOperation),
                nameof(Operator.Exponential) => new ExpService(logger).Calculate(nestedOperation),
                _ => throw new ArgumentOutOfRangeException()
            };
            mul += nestedResult;
        }

        return mul;
    }
}
