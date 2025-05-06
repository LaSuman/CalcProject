using CalculatorProject.Models;
using CalculatorProject.Services;

namespace CalculatorProject.Tests;

public class SubServiceTest
{
    private readonly IOperation _operation = new SubService();

    [Fact(DisplayName = "Should sub two valid number")]
    public async Task ShouldSubTwoValidNumber()
    {
        // Arrange
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Subtraction),
                    Value = new List<string> { "3", "2" }
                }
            }

        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(1.00, result);
    }

    [Fact(DisplayName = "Should sub One valid number ")]
    public async Task ShouldSubOneValidNumber()
    {
        // Arrange
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Subtraction),
                    Value = new List<string> { "3" }
                }
            }

        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(3.00, result);
    }

    [Fact(DisplayName = "Should sub with 0")]
    public async Task ShouldsubWith0()
    {
        // Arrange
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Subtraction),
                    Value = new List<string> { "6", "0" }
                }
            }

        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(6.00, result);
    }

    [Fact(DisplayName = "Should sub negative number ")]
    public async Task ShouldSubNegativeNumber()
    {
        // Arrange
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Subtraction),
                    Value = new List<string> { "-3", "-10" }
                }
            }

        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(7.00, result);
    }

    [Fact(DisplayName = "Should handle large number ")]
    public async Task ShouldHandleLargeNumber()
    {
        // Arrange
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Subtraction),
                    Value = new List<string> { "3000000", "1000000" }
                }
            }

        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(2000000.00, result);
    }

    [Fact(DisplayName = "Should handle decimal number ")]
    public async Task ShouldHandleDecimalNumber()
    {
        // Arrange
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Subtraction),
                    Value = new List<string> { "3.5", "2.5" }
                }
            }
        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(1.00, result);
    }


    [Fact(DisplayName = "Should handle empty input ")]
    public async Task ShouldHandleEmptyInput()
    {
        // Arrange
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Subtraction),
                    Value = new List<string> { }
                }
            }
        };
        // Assert
        Assert.Throws<ArgumentException>(() => _operation.Calculate(request)); ;
    }

    [Fact(DisplayName = "Should throw exception different format input ")]
    public async Task ShouldThrowExceptionDifferentFormatInput()
    {
        // Arrange
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Subtraction),
                    Value = new List<string>() { "1", "abc" }
                }
            }
        };
        // Assert
        Assert.Throws<FormatException>(() => _operation.Calculate(request));
    }

    [Fact(DisplayName = "Should throw exception invalid operation ")]
    public async Task ShouldThrowExceptionInvalidOperation()
    {
        // Arrange
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = "InvalidOperation",
                    Value = new List<string>() { "1", "2" }
                }
            }
        };
        // Assert
        Assert.Throws<InvalidOperationException>(() => _operation.Calculate(request));
    }

    [Fact(DisplayName = "Should throw exception null input ")]
    public async Task ShouldThrowExceptionNullInput()
    {
        // Arrange
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = null
            }
        };
        // Assert
        Assert.Throws<NullReferenceException>(() => _operation.Calculate(request));
    }

    [Fact(DisplayName = "Should calculate leading zeros ")]
    public async Task ShouldCalculateLeadingZeros()
    {
        // Arrange
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Subtraction),
                    Value = new List<string> { "0003", "0002" }
                }
            }
        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(1.00, result);
    }
    [Fact(DisplayName = "Should calculate trailing zeros ")]
    public async Task ShouldCalculateTrailingZeros()
    {
        // Arrange
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Subtraction),
                    Value = new List<string> { "3.00", "2.00" }
                }
            }
        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(1.00, result);
    }
}