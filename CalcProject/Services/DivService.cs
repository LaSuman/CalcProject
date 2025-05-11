using CalculatorProject.Controllers;
using CalculatorProject.Models;

namespace CalculatorProject.Services
{
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

            double div = Calculate(calculatorRequest.Maths.Operation);
            return div;
        }

        public double Calculate(Operation calculatorRequest)
        {
            if (calculatorRequest.Value is { Count: 0 })
                throw new ArgumentException("Division requires at least two values.");
            var div = double.Parse(calculatorRequest.Value[0]);
            if (div == 0)
                throw new DivideByZeroException("Cannot divide by zero.");

            for (var i = 1; i < calculatorRequest.Value.Count; i++)
            {
                var value = double.Parse(calculatorRequest.Value[i] ?? string.Empty);
                div /= value;
            }

            if (calculatorRequest.Value.Count == 1 && calculatorRequest.NestedOperation == null)
                throw new DivideByZeroException("Cannot divide by zero.");

            // Handle nested calculation, if present.
            if (calculatorRequest.NestedOperation == null) return div;

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
                div += nestedResult;
            }

            return div;
        }
    }
}