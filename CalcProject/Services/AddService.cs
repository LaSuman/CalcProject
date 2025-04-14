using CalculatorProject.Models;

namespace CalculatorProject.Services
{
    public class AddService : IOperation
    {
        public double Calculate(CalculatorRequest calculatorRequest)
        {
            double sum = 0;
            foreach (var num in calculatorRequest.Maths.Operation.Value)
            {
                var Value = double.Parse(num);
                sum += Value;
            }

            return sum;
        }
    }
}
