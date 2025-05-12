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
          
        return Calculate(calculatorRequest.Maths.Operation); ;
    }

    public double Calculate(Operation calculatorRequest)
    {
        double sum = 0;
        if (calculatorRequest.Value != null) sum += calculatorRequest.Value.Sum(double.Parse!);

        // Handle nested calculation, if present.
        if (calculatorRequest.NestedOperation == null) return sum;

        sum += calculatorRequest.NestedOperation.Sum(nestedOperation => nestedOperation.ID switch
        {
            nameof(Operator.Plus) => new AddService(logger).Calculate(nestedOperation),
            nameof(Operator.Subtraction) => new SubService(logger).Calculate(nestedOperation),
            nameof(Operator.Multiplication) => new MulService(logger).Calculate(nestedOperation),
            nameof(Operator.Division) => new DivService(logger).Calculate(nestedOperation),
            nameof(Operator.Exponential) => new ExpService(logger).Calculate(nestedOperation),
            _ => throw new ArgumentOutOfRangeException { HelpLink = null, HResult = 0, Source = null }
        });

        return sum;
    }
}
