using CalculatorProject.Models;

namespace CalculatorProject.Services
{
    public class SubService : IOperation
    {
        public double Calculate(CalculatorRequest calculatorRequest)
        {
            int sub = Int32.Parse(calculatorRequest.Maths.Operation.Value[0]);
            for (int i = 1; i < calculatorRequest.Maths.Operation.Value.Count; i++)
            {
                var Value = Int32.Parse(calculatorRequest.Maths.Operation.Value[i]);
                sub -= Value;
            }
            return sub;
        }
    }
}
