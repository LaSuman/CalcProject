using CalculatorProject.Models;

namespace CalculatorProject.Services
{
    public class DivService : IOperation
    {
        public double Calculate(CalculatorRequest calculatorRequest)
        {
            if (calculatorRequest.Maths.Operation == null)
                throw new NullReferenceException();

            if (calculatorRequest.Maths.Operation.ID != nameof(Operator.Division))
                throw new InvalidOperationException();
            double div = Calculate(calculatorRequest.Maths.Operation);
            return div;
        }

        public double Calculate(Operation calculatorRequest)
        {
            if (calculatorRequest.Value.Count == 0)
                throw new ArgumentException("Division requires at least two values.");
            double div = double.Parse(calculatorRequest.Value[0]);
            if (div == 0)
                throw new DivideByZeroException("Cannot divide by zero.");

            for (int i = 1; i < calculatorRequest.Value.Count; i++)
            {
                var value = double.Parse(calculatorRequest.Value[i]);
                div /= value;
            }
            if (calculatorRequest.Value.Count == 1)
                throw new DivideByZeroException("Cannot divide by zero.");


            // Handle nested calculation, if present.
            if (calculatorRequest.NestedOperation != null)
            {
                var nextedResult = calculatorRequest.NestedOperation.ID switch
                {
                    nameof(Operator.Plus) => new AddService().Calculate(calculatorRequest.NestedOperation),
                    nameof(Operator.Subtraction) => new SubService().Calculate(calculatorRequest.NestedOperation),
                    nameof(Operator.Multiplication) => new MulService().Calculate(calculatorRequest.NestedOperation),
                    nameof(Operator.Division) => new DivService().Calculate(calculatorRequest.NestedOperation),
                    _ => throw new ArgumentOutOfRangeException()
                };
                div += nextedResult;
            }

            return div;
        }
    }
}