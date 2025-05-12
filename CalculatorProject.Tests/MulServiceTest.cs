using CalculatorProject.Models;
using CalculatorProject.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace CalculatorProject.Tests;
public class MulServiceTest
{

    private readonly MulService _operation;

    public MulServiceTest()
    {
        // Setup mock logger
        var mockLogger = new Mock<ILogger<MulService>>();
        _operation = new MulService(mockLogger.Object);
    }

    [Fact(DisplayName = "Should return 10 when multiple 5 and 2")]
    public async Task ShouldReturn10WhenMultiple5And2()
    {

        // Arrange
        var request = new CalculatorRequest
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
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Multiplication),
                    Value = ["3", "2"]
                }
            }

        };
        // Act
        var result = _operation.Calculate(request);
        // Assert
        Assert.Equal(6.00, result);
    }

    [Fact(DisplayName = "Should Mul One valid number ")]
    public async Task ShouldMulOneValidNumber()
    {
        // Arrange
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Multiplication),
                    Value = ["3"]
                }
            }

        };
        // Act
        var result = _operation.Calculate(request);
        // Assert
        Assert.Equal(3.00, result);
    }

    [Fact(DisplayName = "Should Mul with 0")]
    public async Task ShouldMulWith0()
    {
        // Arrange
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Multiplication),
                    Value = ["6", "0"]
                }
            }

        };
        // Act
        var result = _operation.Calculate(request);
        // Assert
        Assert.Equal(0.00, result);
    }

    [Fact(DisplayName = "Should mul negative number ")]
    public async Task ShouldMulNegativeNumber()
    {
        // Arrange
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Multiplication),
                    Value = ["-3", "-10"]
                }
            }

        };
        // Act
        var result = _operation.Calculate(request);
        // Assert
        Assert.Equal(30.00, result);
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
                    ID = nameof(Operator.Multiplication),
                    Value = ["3000000", "1000000"]
                }
            }

        };
        // Act
        var result = _operation.Calculate(request);
        // Assert
        Assert.Equal(3000000000000.00, result);
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
                    ID = nameof(Operator.Multiplication),
                    Value = ["2.5", "2.5"]
                }
            }
        };
        // Act
        var result = _operation.Calculate(request);
        // Assert
        Assert.Equal(6.25, result);
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
                    ID = nameof(Operator.Multiplication),
                    Value = []
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
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Multiplication),
                    Value = ["1", "abc"]
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
                    Value = ["1", "2"]
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
                    ID = nameof(Operator.Multiplication),
                    Value = ["0003", "0002"]
                }
            }
        };
        // Act
        var result = _operation.Calculate(request);
        // Assert
        Assert.Equal(6.00, result);
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
                    ID = nameof(Operator.Multiplication),
                    Value = ["3.00", "2.00"]
                }
            }
        };
        // Act
        var result = _operation.Calculate(request);
        // Assert
        Assert.Equal(6.00, result);
    }
}