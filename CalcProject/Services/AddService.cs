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
        if (calculatorRequest.Value != null) sum += calculatorRequest.Value.Sum(double.Parse!);

        // Handle nested calculation, if present.
        if (calculatorRequest.NestedOperation == null) return sum;

        double sum1 = 0;
        foreach (var nestedOperation in calculatorRequest.NestedOperation)
            sum1 += nestedOperation.ID switch
            {
                nameof(Operator.Plus) => new AddService(logger).Calculate((Operation)nestedOperation),
                nameof(Operator.Subtraction) => new SubService(logger).Calculate((Operation)nestedOperation),
                nameof(Operator.Multiplication) => new MulService(logger).Calculate((Operation)nestedOperation),
                nameof(Operator.Division) => new DivService(logger).Calculate((Operation)nestedOperation),
                nameof(Operator.Exponential) => new ExpService(logger).Calculate((Operation)nestedOperation),
                _ => throw new ArgumentOutOfRangeException { HelpLink = null, HResult = 0, Source = null }
            };

        sum += sum1;

        return sum;
    }
}
