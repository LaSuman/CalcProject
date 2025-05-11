using CalculatorProject.Controllers;
using CalculatorProject.Models;

namespace CalculatorProject.Services
{
    public class SubService(ILogger logger) : BaseService
    {
        public override double Calculate(CalculatorRequest calculatorRequest)
        {
            logger.LogInformation("Performing subtraction for values: {Values}",
                calculatorRequest.Maths?.Operation?.Value);

            if (calculatorRequest.Maths?.Operation == null)
                throw new NullReferenceException();

            if (calculatorRequest.Maths.Operation.ID != nameof(Operator.Subtraction))
                throw new InvalidOperationException();
            var sub = Calculate(calculatorRequest.Maths.Operation);
            return sub;
        }

        public double Calculate(Operation calculatorRequest)
        {
            if (calculatorRequest.Value is { Count: 0 })
                throw new ArgumentException("Subtraction requires at least two values.");

            var sub = double.Parse(calculatorRequest.Value?[0] ?? string.Empty);

            if (calculatorRequest.Value != null)
                for (var i = 1; i < calculatorRequest.Value.Count; i++)
                {
                    var value = double.Parse(calculatorRequest.Value[i] ?? string.Empty);
                    sub -= value;
                }

            // Handle nested calculation, if present.
            if (calculatorRequest.NestedOperation == null) return sub;

            double sum = 0;
            foreach (var nestedOperation in calculatorRequest.NestedOperation)
                sum += nestedOperation.ID switch
                {
                    nameof(Operator.Plus) => new AddService(logger).Calculate((Operation)nestedOperation),
                    nameof(Operator.Subtraction) => new SubService(logger).Calculate((Operation)nestedOperation),
                    nameof(Operator.Multiplication) => new MulService(logger).Calculate((Operation)nestedOperation),
                    nameof(Operator.Division) => new DivService(logger).Calculate((Operation)nestedOperation),
                    nameof(Operator.Exponential) => new ExpService(logger).Calculate((Operation)nestedOperation),
                    _ => throw new ArgumentOutOfRangeException { HelpLink = null, HResult = 0, Source = null }
                };

            sub += sum;

            return sub;
        }
    }
}