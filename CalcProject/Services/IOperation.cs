using CalculatorProject.Models;

namespace CalculatorProject.Services;

public interface IOperation
{
    public double Calculate(CalculatorRequest calculatorRequest);
}

