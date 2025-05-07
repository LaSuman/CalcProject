using CalculatorProject.Models;

namespace CalculatorProject.Services;

internal class ExpService : IOperation
{
    public double Calculate(CalculatorRequest calculatorRequest)
    {
        if (calculatorRequest?.Maths?.Operation == null)
            throw new ArgumentNullException(nameof(calculatorRequest), "CalculatorRequest, Maths, or Operation cannot be null.");

        return Calculate(calculatorRequest.Maths.Operation);
    }

    public double Calculate(Operation op)
    {
        if (op?.Value == null || op.Value.Count != 2)
            throw new InvalidOperationException("Exponential requires exactly two operands.");
        if (op.Value[0] == "e" || op.Value[0] == "E")
        {
            op.Value[0] = 2.718281828459045.ToString(); 
        }
        var baseNum = double.Parse(op.Value[0]);
        var exponent = double.Parse(op.Value[1]);

        return Math.Pow(baseNum, exponent);
    }
}
