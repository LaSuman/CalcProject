using CalculatorProject.Models;

namespace CalculatorProject.Services
{
    public class AddService : IOperation
    {
        public double Calculate(CalculatorRequest calculatorRequest)
        {
            int sum = 0;
            foreach (var num in calculatorRequest.Maths.Operation.Value)
            {
                var Value = int.Parse(num);
                sum += Value;
            }

            return sum;
        }
    }
}
