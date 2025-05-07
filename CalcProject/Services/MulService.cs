using CalculatorProject.Controllers;
using CalculatorProject.Models;

namespace CalculatorProject.Services;

public class MulService : IOperation
{
    public double Calculate(CalculatorRequest calculatorRequest)
    {
        if (calculatorRequest.Maths?.Operation == null)
            throw new NullReferenceException();

        if (calculatorRequest.Maths.Operation.ID != nameof(Operator.Multiplication))
            throw new InvalidOperationException();
        double mul = Calculate(calculatorRequest.Maths.Operation);
        return mul;
    }


    public double Calculate(Operation calculatorRequest)
    {
        var mul = double.Parse(calculatorRequest.Value[0]);

        for (var i = 1; i < calculatorRequest.Value.Count; i++)
        {

            var value = double.Parse(calculatorRequest.Value[i]);
            mul *= value;
        }
        if (calculatorRequest.Value.Count == 1 && calculatorRequest.NestedOperation == null)
            mul = 0;

        // Handle nested calculation, if present.
        if (calculatorRequest.NestedOperation == null) return mul;
        var nestedResult = calculatorRequest.NestedOperation.ID switch
        {
            nameof(Operator.Plus) => new AddService().Calculate(calculatorRequest.NestedOperation),
            nameof(Operator.Subtraction) => new SubService().Calculate(calculatorRequest.NestedOperation),
            nameof(Operator.Multiplication) => new MulService().Calculate(calculatorRequest.NestedOperation),
            nameof(Operator.Division) => new DivService().Calculate(calculatorRequest.NestedOperation),
            nameof(Operator.Exponential) => new ExpService().Calculate(calculatorRequest.NestedOperation),
            _ => throw new ArgumentOutOfRangeException()
        };
        mul += nestedResult;

        return mul;
    }
}

