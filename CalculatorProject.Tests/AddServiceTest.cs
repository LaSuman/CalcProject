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
    }
}
