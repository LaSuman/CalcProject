using CalculatorProject.Models;
using CalculatorProject.Services;

namespace CalculatorProject.Tests
{

    public class SubServiceTest
    {
        private IOperation operation;

        public SubServiceTest()
        {
            operation = new SubService();
        }

        [Fact(DisplayName = "Should sub two valid number")]
        public async Task ShouldSubTwoValidNumber()
        {
            // Arrange
            CalculatorRequest request = new CalculatorRequest
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
            double result = operation.Calculate(request);
            // Assert
            Assert.Equal(1.00, result);
        }

        [Fact(DisplayName = "Should sub One valid number ")]
        public async Task ShouldSubOneValidNumber()
        {
            // Arrange
            CalculatorRequest request = new CalculatorRequest
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
            double result = operation.Calculate(request);
            // Assert
            Assert.Equal(3.00, result);
        }

        [Fact(DisplayName = "Should sub with 0")]
        public async Task ShouldsubWith0()
        {
            // Arrange
            CalculatorRequest request = new CalculatorRequest
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
            double result = operation.Calculate(request);
            // Assert
            Assert.Equal(6.00, result);
        }

        [Fact(DisplayName = "Should sub negative number ")]
        public async Task ShouldSubNegativeNumber()
        {
            // Arrange
            CalculatorRequest request = new CalculatorRequest
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
            double result = operation.Calculate(request);
            // Assert
            Assert.Equal(7.00, result);
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
                        ID = nameof(Operator.Subtraction),
                        Value = new List<string> { "3000000", "1000000" }
                    }
                }

            };
            // Act
            double result = operation.Calculate(request);
            // Assert
            Assert.Equal(2000000.00, result);
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
                        ID = nameof(Operator.Subtraction),
                        Value = new List<string> { "3.5", "2.5" }
                    }
                }
            };
            // Act
            double result = operation.Calculate(request);
            // Assert
            Assert.Equal(1.00, result);
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
                        ID = nameof(Operator.Subtraction),
                        Value = new List<string> { }
                    }
                }
            };
            // Assert
            Assert.Throws<ArgumentException>(() => operation.Calculate(request));;
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
                        ID = nameof(Operator.Subtraction),
                        Value = new List<string>() { "1", "abc" }
                    }
                }
            };
            // Assert
            Assert.Throws<FormatException>(() => operation.Calculate(request));
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
            Assert.Throws<InvalidOperationException>(() => operation.Calculate(request));
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
            Assert.Throws<NullReferenceException>(() => operation.Calculate(request));
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
                        ID = nameof(Operator.Subtraction),
                        Value = new List<string> { "0003", "0002" }
                    }
                }
            };
            // Act
            double result = operation.Calculate(request);
            // Assert
            Assert.Equal(1.00, result);
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
                        ID = nameof(Operator.Subtraction),
                        Value = new List<string> { "3.00", "2.00" }
                    }
                }
            };
            // Act
            double result = operation.Calculate(request);
            // Assert
            Assert.Equal(1.00, result);
        }
    }
}
