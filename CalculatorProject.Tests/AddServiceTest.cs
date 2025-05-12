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
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Plus),
                    Value = ["2", "3"]
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
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Plus),
                    Value = ["3"]
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
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Plus),
                    Value = ["6", "0"]
                }
            }

        };
        // Act
        var result = _operation.Calculate(request);
        // Assert
        Assert.Equal(6.00, result);
    }


    [Fact(DisplayName = "Should add negative number ")]
    public async Task ShouldAddNegativeNumber()
    {
        // Arrange
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Plus),
                    Value = ["-3", "-10"]
                }
            }

        };
        // Act
        var result = _operation.Calculate(request);
        // Assert
        Assert.Equal(-13.00, result);
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
                    ID = nameof(Operator.Plus),
                    Value = ["30000000", "1000000"]
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
        var request = new CalculatorRequest
        {
            Maths = new Maths
            {
                Operation = new Operation
                {
                    ID = nameof(Operator.Plus),
                    Value = ["3.5", "2.5"]
                }
            }
        };
        // Act
        var result = _operation.Calculate(request);
        // Assert
        Assert.Equal(6.00, result);
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
                    ID = nameof(Operator.Plus),
                    Value = []
                }
            }
        };
        // Act
        var result = _operation.Calculate(request);
        // Assert
        Assert.Equal(0.00, result);
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
                    ID = nameof(Operator.Plus),
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
                    ID = nameof(Operator.Plus),
                    Value = ["0003", "0002"]
                }
            }
        };
        // Act
        var result = _operation.Calculate(request);
        // Assert
        Assert.Equal(5.00, result);
    }
}
