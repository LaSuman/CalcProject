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
        [Fact(DisplayName = "Should return 2 when subtract from 5 and 3")]
        public async Task ShouldReturn2WhenSubtractFrom5And3()
        {

            // Arrange
            CalculatorRequest request = new CalculatorRequest
            {
                Maths = new Maths
                {
                    Operation = new Operation
                    {
                        ID = nameof(Operator.Subtraction),
                        Value = ["5", "3"]
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
