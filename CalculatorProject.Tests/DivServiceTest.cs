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
    }
}