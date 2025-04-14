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
            double div = double.Parse(calculatorRequest.Value[0]);

            for (int i = 1; i < calculatorRequest.Value.Count; i++)
            {
                var value = double.Parse(calculatorRequest.Value[i]);
                div /= value;
            }

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