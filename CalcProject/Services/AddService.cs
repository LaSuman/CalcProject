using CalculatorProject.Models;

namespace CalculatorProject.Services
{
    public class AddService : IOperation
    {
        public double Calculate(CalculatorRequest calculatorRequest)
        {
            double sum = 0;
            sum = Calculate(calculatorRequest.Maths.Operation);

            return sum;
        }

        public double Calculate(Operation calculatorRequest)
        {
            double sum = 0;
            foreach (var addValue in calculatorRequest.Value)
            {
                var value = Int32.Parse(addValue);
                sum += value;
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
                sum += nextedResult;
            }
            return sum;
        }
    }
}