using CalculatorProject.Models;
using CalculatorProject.Services;

namespace CalculatorProject.Tests
{
    public class AddServiceTest
    {
        private IOperation operation;

        public AddServiceTest()
        {
            operation = new AddService();
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
            double result = operation.Calculate(request);
            // Assert
            Assert.Equal(5.00, result);
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
            double result = operation.Calculate(request);
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
                        Value = new List<string> { "6","0" }
                    }
                }

            };
            // Act
            double result = operation.Calculate(request);
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
                        Value = new List<string> { "-3","-10" }
                    }
                }

            };
            // Act
            double result = operation.Calculate(request);
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
                        Value = new List<string> { "30000000","1000000" }
                    }
                }

            };
            // Act
            double result = operation.Calculate(request);
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
            double result = operation.Calculate(request);
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
                        Value = new List<string> {}
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
                        ID = nameof(Operator.Plus),
                        Value = new List<string>() {"1","abc" }
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
                        ID = nameof(Operator.Plus),
                        Value = new List<string> { "0003", "0002" }
                    }
                }
            };
            // Act
            double result = operation.Calculate(request);
            // Assert
            Assert.Equal(5.00, result);
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
                        ID = nameof(Operator.Plus),
                        Value = new List<string> { "3.00", "2.00" }
                    }
                }
            };
            // Act
            double result = operation.Calculate(request);
            // Assert
            Assert.Equal(5.00, result);
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
                        ID = nameof(Operator.Plus),
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
