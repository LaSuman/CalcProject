using CalculatorProject.Controllers;
using CalculatorProject.Models;

namespace CalculatorProject.Services;

public class AddService(ILogger logger) : BaseService()
{
   

    public override double Calculate(CalculatorRequest calculatorRequest)
    {
        logger.LogInformation("Performing addition for values: {Values}", calculatorRequest.Maths?.Operation?.Value);

        if (calculatorRequest.Maths?.Operation == null)
            throw new NullReferenceException();

        if (calculatorRequest.Maths.Operation.ID != nameof(Operator.Plus))
            throw new InvalidOperationException();

        double sum = 0;
        sum = Calculate(calculatorRequest.Maths.Operation);

        return sum;
    }

    public double Calculate(Operation calculatorRequest)
    {
        double sum = 0;
        if (calculatorRequest.Value != null) sum += calculatorRequest.Value.Sum(addValue => double.Parse(addValue));

        // Handle nested calculation, if present.
        if (calculatorRequest.NestedOperation == null) return sum;
        var nestedResult = calculatorRequest.NestedOperation.ID switch
        {
            nameof(Operator.Plus) => new AddService(logger).Calculate(calculatorRequest.NestedOperation),
            nameof(Operator.Subtraction) => new SubService(logger).Calculate(calculatorRequest.NestedOperation),
            nameof(Operator.Multiplication) => new MulService(logger).Calculate(calculatorRequest.NestedOperation),
            nameof(Operator.Division) => new DivService(logger).Calculate(calculatorRequest.NestedOperation),
            nameof(Operator.Exponential) => new ExpService(logger).Calculate(calculatorRequest.NestedOperation),
            _ => throw new ArgumentOutOfRangeException()
        };
        sum += nestedResult;

        return sum;
    }
}
