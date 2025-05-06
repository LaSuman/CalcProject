using CalculatorProject.Models;
using CalculatorProject.Services;

namespace CalculatorProject.Tests;
public class MulServiceTest
{
    private readonly IOperation _operation = new MulService();

    [Fact(DisplayName = "Should return 10 when multiple 5 and 2")]
    public async Task ShouldReturn10WhenMultiple5And2()
    {

        // Arrange
        CalculatorRequest request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Multiplication),
                    Value = ["5", "2"]
                }
            }
        };

        // Act
        var result = _operation.Calculate(request);
        // Assert
        Assert.Equal(10.0, result);

    }
    [Fact(DisplayName = "Should mul two valid number")]
    public async Task ShouldMulTwoValidNumber()
    {
        // Arrange
        CalculatorRequest request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Multiplication),
                    Value = new List<string> { "3", "2" }
                }
            }

        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(6.00, result);
    }

    [Fact(DisplayName = "Should Mul One valid number ")]
    public async Task ShouldMulOneValidNumber()
    {
        // Arrange
        CalculatorRequest request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Multiplication),
                    Value = new List<string> { "3" }
                }
            }

        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(0.00, result);
    }

    [Fact(DisplayName = "Should Mul with 0")]
    public async Task ShouldMulWith0()
    {
        // Arrange
        CalculatorRequest request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Multiplication),
                    Value = new List<string> { "6", "0" }
                }
            }

        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(0.00, result);
    }

    [Fact(DisplayName = "Should mul negative number ")]
    public async Task ShouldMulNegativeNumber()
    {
        // Arrange
        CalculatorRequest request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Multiplication),
                    Value = new List<string> { "-3", "-10" }
                }
            }

        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(30.00, result);
    }

    [Fact(DisplayName = "Should handle large number ")]
    public async Task ShouldHandleLargeNumber()
    {
        // Arrange
        CalculatorRequest request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Multiplication),
                    Value = new List<string> { "3000000", "1000000" }
                }
            }

        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(3000000000000.00, result);
    }

    [Fact(DisplayName = "Should handle decimal number ")]
    public async Task ShouldHandleDecimalNumber()
    {
        // Arrange
        CalculatorRequest request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Multiplication),
                    Value = new List<string> { "2.5", "2.5" }
                }
            }
        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(6.25, result);
    }


    [Fact(DisplayName = "Should handle empty input ")]
    public async Task ShouldHandleEmptyInput()
    {
        // Arrange
        CalculatorRequest request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Multiplication),
                    Value = new List<string> { }
                }
            }
        };
        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => _operation.Calculate(request));
    }

    [Fact(DisplayName = "Should throw exception different format input ")]
    public async Task ShouldThrowExceptionDifferentFormatInput()
    {
        // Arrange
        CalculatorRequest request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Multiplication),
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
        CalculatorRequest request = new CalculatorRequest
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
        CalculatorRequest request = new CalculatorRequest
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
        CalculatorRequest request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Multiplication),
                    Value = new List<string> { "0003", "0002" }
                }
            }
        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(6.00, result);
    }
    [Fact(DisplayName = "Should calculate trailing zeros ")]
    public async Task ShouldCalculateTrailingZeros()
    {
        // Arrange
        CalculatorRequest request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Multiplication),
                    Value = new List<string> { "3.00", "2.00" }
                }
            }
        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(6.00, result);
    }
}