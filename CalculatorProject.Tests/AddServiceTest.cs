using CalculatorProject.Models;
using CalculatorProject.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace CalculatorProject.Tests;

public class AddServiceTest
{
    private readonly AddService _operation;

    public AddServiceTest()
    {
        // Setup mock logger
        var mockLogger = new Mock<ILogger<AddService>>();
        _operation = new AddService(mockLogger.Object);
    }

    [Fact(DisplayName = "Should add two valid number")]
    public async Task ShouldAddTwoValidNumber()
    {
        // Arrange
        CalculatorRequest request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Plus),
                    Value = new List<string> { "2", "3" }
                }
            }
        };

        // Act
        var result = _operation.Calculate(request);
        // Assert
        Assert.Equal(5.0, result);
    }

    [Fact(DisplayName = "Should add One valid number ")]
    public async Task ShouldAddOneValidNumber()
    {
        // Arrange
        CalculatorRequest request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Plus),
                    Value = new List<string> { "3" }
                }
            }

        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(3.00, result);
    }

    [Fact(DisplayName = "Should add with 0")]
    public async Task ShouldAddWith0()
    {
        // Arrange
        CalculatorRequest request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Plus),
                    Value = new List<string> { "6", "0" }
                }
            }

        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(6.00, result);
    }


    [Fact(DisplayName = "Should add negative number ")]
    public async Task ShouldAddNegativeNumber()
    {
        // Arrange
        CalculatorRequest request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Plus),
                    Value = new List<string> { "-3", "-10" }
                }
            }

        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(-13.00, result);
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
                    ID = nameof(Operator.Plus),
                    Value = new List<string> { "30000000", "1000000" }
                }
            }

        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(31000000.00, result);
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
                    ID = nameof(Operator.Plus),
                    Value = new List<string> { "3.5", "2.5" }
                }
            }
        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(6.00, result);
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
                    ID = nameof(Operator.Plus),
                    Value = new List<string> { }
                }
            }
        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(0.00, result);
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
                    ID = nameof(Operator.Plus),
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
                    ID = nameof(Operator.Plus),
                    Value = new List<string> { "0003", "0002" }
                }
            }
        };
        // Act
        double result = _operation.Calculate(request);
        // Assert
        Assert.Equal(5.00, result);
    }

}
