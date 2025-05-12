using CalculatorProject.Models;
using CalculatorProject.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace CalculatorProject.Tests;

public class DivServiceTest
{

    private readonly DivService _operation;

    public DivServiceTest()
    {
        // Setup mock logger
        var mockLogger = new Mock<ILogger<DivService>>();
        _operation = new DivService(mockLogger.Object);
    }

    [Fact(DisplayName = "Should return 2 when divide 6 by 3")]
    public async Task ShouldRetun2WhenDivide6By3()
    {
        // Arrange
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Division),
                    Value = ["6", "3"]
                }
            }
        };

        // Act
        var result = _operation.Calculate(request);
        // Assert
        Assert.Equal(2.0, result);
    }
    [Fact(DisplayName = "Should div two valid number")]
    public async Task ShouldDivTwoValidNumber()
    {
        // Arrange
        CalculatorRequest request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Division),
                    Value = new List<string> { "6", "2" }
                }
            }

        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(3.00, result);
    }

    [Fact(DisplayName = "Should Error for One input ")]
    public async Task ShouldErrorForOneInput()
    {
        // Arrange
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Division),
                    Value = new List<string> { "3" }
                }
            }

        };
        // Assert
        Assert.Throws<DivideByZeroException>(() => _operation.Calculate(request));
    }

    [Fact(DisplayName = "Should div with 0 as infinity")]
    public async Task ShoulddivWith0AsInfinity()
    {
        // Arrange
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Division),
                    Value = new List<string> { "6", "0" }
                }
            }

        };
        // Assert
        Assert.Throws<DivideByZeroException>(() => _operation.Calculate(request));
    }

    [Fact(DisplayName = "Should div negative number ")]
    public async Task ShouldDivNegativeNumber()
    {
        // Arrange
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Division),
                    Value = new List<string> { "-30", "-3" }
                }
            }

        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(10.00, result);
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
                    ID = nameof(Operator.Division),
                    Value = new List<string> { "3000000", "1000000" }
                }
            }

        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(3, result);
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
                    ID = nameof(Operator.Division),
                    Value = new List<string> { "15.0", "1.5" }
                }
            }
        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(10.00, result);
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
                    ID = nameof(Operator.Division),
                    Value = new List<string> { }
                }
            }
        };
        // Assert
        Assert.Throws<DivideByZeroException>(() => _operation.Calculate(request));
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
                    ID = nameof(Operator.Division),
                    Value = new List<string>() { "1", "abc" }
                }
            }
        };
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
                    ID = nameof(Operator.Division),
                    Value = new List<string> { "0003", "0002" }
                }
            }
        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(1.5, result);
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
                    ID = nameof(Operator.Division),
                    Value = new List<string> { "3.00", "2.00" }
                }
            }
        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(1.5, result);
    }
}