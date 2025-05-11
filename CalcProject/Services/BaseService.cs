using CalculatorProject.Models;

namespace CalculatorProject.Services
{
    public abstract class BaseService() : IOperation
    {
        public abstract double Calculate(CalculatorRequest calculatorRequest);
        protected double ParseValue(string value)
        {
            return value.ToLower() == "e" ? Math.E : double.Parse(value);
        }
    }
}
