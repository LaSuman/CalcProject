using CalculatorProject.Models;
using CalculatorProject.Services;

namespace CalculatorProject.Tests
{
    public class MulServiceTest
    {
        private IOperation operation;

        public MulServiceTest()
        {
            operation = new MulService();
        }

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
            var result = operation.Calculate(request);
            // Assert
            Assert.Equal(10.0, result);

        }
    }
}
