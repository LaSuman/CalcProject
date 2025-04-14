using CalculatorProject.Models;
using CalculatorProject.Services;

namespace CalculatorProject.Tests
{
    public class DivServiceTest
    {
        private IOperation operation;

        public DivServiceTest()
        {
            operation = new DivService();
        }

        [Fact(DisplayName = "Should return 2 when divide 6 by 3")]
        public async Task ShouldRetun2WhenDivide6By3()
        {
            // Arrange
            CalculatorRequest request = new CalculatorRequest
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
            var result = operation.Calculate(request);
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
            double result = operation.Calculate(request);
            // Assert
            Assert.Equal(3.00, result);
        }

        [Fact(DisplayName = "Should Error for One input ")]
        public async Task ShouldErrorForOneInput()
        {
            // Arrange
            CalculatorRequest request = new CalculatorRequest
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
            // Act
            double result = operation.Calculate(request);
            // Assert
            Assert.Equal(3.00, result);
        }

        [Fact(DisplayName = "Should div with 0 as infinity")]
        public async Task ShoulddivWith0AsInfinity()
        {
            // Arrange
            CalculatorRequest request = new CalculatorRequest
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
            // Act
            double result = operation.Calculate(request);
            // Assert
            Assert.Equal(1, result);
        }

        [Fact(DisplayName = "Should div negative number ")]
        public async Task ShouldDivNegativeNumber()
        {
            // Arrange
            CalculatorRequest request = new CalculatorRequest
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
            double result = operation.Calculate(request);
            // Assert
            Assert.Equal(10.00, result);
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
                        ID = nameof(Operator.Division),
                        Value = new List<string> { "3000000", "1000000" }
                    }
                }

            };
            // Act
            double result = operation.Calculate(request);
            // Assert
            Assert.Equal(3, result);
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
                        ID = nameof(Operator.Division),
                        Value = new List<string> { "15.0", "1.5" }
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
                        ID = nameof(Operator.Division),
                        Value = new List<string> { }
                    }
                }
            };
            // Act
            double result = operation.Calculate(request);
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
                        ID = nameof(Operator.Division),
                        Value = new List<string>() { "1", "abc" }
                    }
                }
            };
            // Act
            double result = operation.Calculate(request);
            // Assert
            Assert.Throws<FormatException>(() => result);
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
            // Act
            double result = operation.Calculate(request);
            // Assert
            Assert.Throws<InvalidOperationException>(() => result);
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
            // Act
            double result = operation.Calculate(request);
            // Assert
            Assert.Throws<ArgumentNullException>(() => result);
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
                        ID = nameof(Operator.Division),
                        Value = new List<string> { "0003", "0002" }
                    }
                }
            };
            // Act
            double result = operation.Calculate(request);
            // Assert
            Assert.Equal(1.5, result);
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
                        ID = nameof(Operator.Division),
                        Value = new List<string> { "3.00", "2.00" }
                    }
                }
            };
            // Act
            double result = operation.Calculate(request);
            // Assert
            Assert.Equal(1.5, result);
        }

        [Fact(DisplayName = "Should skip empty values and calculate valid ones")]
        public async Task ShouldSkipEmptyValues()
        {
            // Arrange
            var request = new CalculatorRequest
            {
                Maths = new Maths
                {
                    Operation = new Operation
                    {
                        ID = nameof(Operator.Division),
                        Value = new List<string> { "5", "", "10" }
                    }
                }
            };

            // Act
            double result = operation.Calculate(request);

            // Assert
            Assert.Equal(15.00, result); // Empty string is treated as 0 or skipped
        }
    }
}