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
    }
}
