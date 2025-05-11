using CalculatorProject.Models;

namespace CalculatorProject.Services
{
    public abstract class BaseService() : IOperation
    {
        public abstract double Calculate(CalculatorRequest calculatorRequest);
         
    }
}
