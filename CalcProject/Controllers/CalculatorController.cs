using CalculatorProject.Models;
using CalculatorProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CalculatorController(ILogger<CalculatorController> logger) : ControllerBase
{
    [HttpPost()]
    [Route("CalculateJson")]
    [Produces("application/json")]
    public IActionResult CalculateJson([FromBody] CalculatorRequest request)
    {
       
        try
        {
            var op = request.Maths?.Operation;

            logger.LogInformation("Received JSON calculation request for operator: {Operator}", op?.ID);
            var result = op?.ID switch
            {
                nameof(Operator.Plus) => new AddService(logger).Calculate(request),
                nameof(Operator.Subtraction) => new SubService(logger).Calculate(request),
                nameof(Operator.Multiplication) => new MulService(logger).Calculate(request),
                nameof(Operator.Division) => new DivService(logger).Calculate(request),
                nameof(Operator.Exponential) => new ExpService(logger).Calculate(request),
                _ => throw new ArgumentOutOfRangeException
                {
                    HelpLink = null,
                    HResult = 0,
                    Source = null
                }
            };

            logger.LogInformation("Calculation successful. Result: {Result}", result);

            return Ok(new CustomResponse
            { 
                Success = true,
                Message = $"Operation successful.",
                Result = result
            });
        }
        catch (FormatException fex)
        {
            logger.LogWarning(fex, "Number format issue: {Message}", fex.Message);
            return BadRequest(new CustomResponse
            {
                Success = false,
                Message = $"Invalid number format: {fex.Message}",
                Result = 0
            });
        }
        catch (DivideByZeroException dbz)
        {
            logger.LogWarning(dbz, "Division by zero occurred.");
            return BadRequest(new CustomResponse
            {
                Success = false,
                Message = "Division by zero error.",
                Result = 0
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error occurred during calculation.");
            return BadRequest(new CustomResponse
            {
                Success = false,
                Message = $"Unexpected error: {ex.Message}",
                Result = 0
            });
        }
    }


    [HttpPost()]
    [Route("CalculateXml")]
    [Consumes("application/xml")]
    [Produces("application/xml")]
    public Task<IActionResult> CalculateXml([FromBody] CalculatorRequestXml xmlRequest)
    {

        try
        {
            var request = xmlRequest.ToCalculatorRequest();
            var op = request.Maths?.Operation;

            logger.LogInformation("Received XML calculation request for operator: {Operator}", op?.ID);

            var result = op?.ID switch
            {
                nameof(Operator.Plus) => new AddService(logger).Calculate(request),
                nameof(Operator.Subtraction) => new SubService(logger).Calculate(request),
                nameof(Operator.Multiplication) => new MulService(logger).Calculate(request),
                nameof(Operator.Division) => new DivService(logger).Calculate(request),
                nameof(Operator.Exponential) => new ExpService(logger).Calculate(request),
                _ => throw new ArgumentOutOfRangeException()
            };
            logger.LogInformation("Calculation successful. Result: {Result}", result);
             
            return Task.FromResult<IActionResult>(Ok(new CustomResponse
            {
                Success = true,
                Message = $"Operation successful.",
                Result = result
            }));
        }

        catch (FormatException fex)
        {
            logger.LogWarning(fex, "Number format issue: {Message}", fex.Message);
            return Task.FromResult<IActionResult>(BadRequest(new CustomResponse
            {
                Success = false,
                Message = $"Invalid number format: {fex.Message}",
                Result = 0
            }));
        }
        catch (DivideByZeroException dbz)
        {
            logger.LogWarning(dbz, "Division by zero occurred.");
            return Task.FromResult<IActionResult>(BadRequest(new CustomResponse
            {
                Success = false,
                Message = "Division by zero error.",
                Result = 0
            }));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error occurred during calculation.");
            return Task.FromResult<IActionResult>(BadRequest(new CustomResponse
            {
                Success = false,
                Message = $"Unexpected error: {ex.Message}",
                Result = 0
            }));
        }
    }
}