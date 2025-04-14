using CalculatorProject.Models;

namespace CalculatorProject.Services
{
    public class MulService : IOperation
    {
        public double Calculate(CalculatorRequest calculatorRequest)
        {
            if (calculatorRequest.Maths.Operation == null)
                throw new NullReferenceException();

            if (calculatorRequest.Maths.Operation.ID != nameof(Operator.Multiplication))
                throw new InvalidOperationException();
            double mul = Calculate(calculatorRequest.Maths.Operation);
            return mul;
        }


        public double Calculate(Operation calculatorRequest)
        {
            double mul = double.Parse(calculatorRequest.Value[0]);

            for (int i = 1; i < calculatorRequest.Value.Count; i++)
            {
                var value = double.Parse(calculatorRequest.Value[i]);
                mul *= value;
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
                mul += nextedResult;
            }

            return mul;
        }
    }
    }
