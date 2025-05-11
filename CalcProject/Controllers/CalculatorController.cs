using CalculatorProject.Models;
using CalculatorProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CalculatorController(ILogger<CalculatorController> logger) : ControllerBase
{
    private readonly ILogger<CalculatorController> _logger = logger;

    [HttpPost()]
    [Route("CalculateJSON")]
    [Produces("application/json")]
    public IActionResult CalculateJSON([FromBody] CalculatorRequest request)
    {
       
        try
        {
            var op = request.Maths?.Operation;

            _logger.LogInformation("Received JSON calculation request for operator: {Operator}", op.ID);
            var result = op.ID switch
            {
                nameof(Operator.Plus) => new AddService(_logger).Calculate(request),
                nameof(Operator.Subtraction) => new SubService(_logger).Calculate(request),
                nameof(Operator.Multiplication) => new MulService(_logger).Calculate(request),
                nameof(Operator.Division) => new DivService(_logger).Calculate(request),
                nameof(Operator.Exponential) => new ExpService(_logger).Calculate(request),
                _ => throw new ArgumentOutOfRangeException
                {
                    HelpLink = null,
                    HResult = 0,
                    Source = null
                }
            };

            _logger.LogInformation("Calculation successful. Result: {Result}", result);

            return Ok(new CustomResponse
            { 
                Success = true,
                Message = $"Operation successful.",
                Result = result
            });
        }
        catch (FormatException fex)
        {
            _logger.LogWarning(fex, "Number format issue: {Message}", fex.Message);
            return BadRequest(new CustomResponse
            {
                Success = false,
                Message = $"Invalid number format: {fex.Message}",
                Result = 0
            });
        }
        catch (DivideByZeroException dbz)
        {
            _logger.LogWarning(dbz, "Division by zero occurred.");
            return BadRequest(new CustomResponse
            {
                Success = false,
                Message = "Division by zero error.",
                Result = 0
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred during calculation.");
            return BadRequest(new CustomResponse
            {
                Success = false,
                Message = $"Unexpected error: {ex.Message}",
                Result = 0
            });
        }
    }


    [HttpPost()]
    [Route("CalculateXML")]
    [Consumes("application/xml")]
    [Produces("application/xml")]
    public Task<IActionResult> CalculateXML([FromBody] CalculatorRequestXml xmlRequest)
    {

        try
        {
            var request = xmlRequest.ToCalculatorRequest();
            var op = request.Maths?.Operation;

            _logger.LogInformation("Received XML calculation request for operator: {Operator}", op?.ID);

            var result = op?.ID switch
            {
                nameof(Operator.Plus) => new AddService(_logger).Calculate(request),
                nameof(Operator.Subtraction) => new SubService(_logger).Calculate(request),
                nameof(Operator.Multiplication) => new MulService(_logger).Calculate(request),
                nameof(Operator.Division) => new DivService(_logger).Calculate(request),
                nameof(Operator.Exponential) => new ExpService(_logger).Calculate(request),
                _ => throw new ArgumentOutOfRangeException
                {
                    HelpLink = null,
                    HResult = 0,
                    Source = null
                }
            };
            _logger.LogInformation("Calculation successful. Result: {Result}", result);
             
            return Task.FromResult<IActionResult>(Ok(new CustomResponse
            {
                Success = true,
                Message = $"Operation successful.",
                Result = result
            }));
        }

        catch (FormatException fex)
        {
            _logger.LogWarning(fex, "Number format issue: {Message}", fex.Message);
            return Task.FromResult<IActionResult>(BadRequest(new CustomResponse
            {
                Success = false,
                Message = $"Invalid number format: {fex.Message}",
                Result = 0
            }));
        }
        catch (DivideByZeroException dbz)
        {
            _logger.LogWarning(dbz, "Division by zero occurred.");
            return Task.FromResult<IActionResult>(BadRequest(new CustomResponse
            {
                Success = false,
                Message = "Division by zero error.",
                Result = 0
            }));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred during calculation.");
            return Task.FromResult<IActionResult>(BadRequest(new CustomResponse
            {
                Success = false,
                Message = $"Unexpected error: {ex.Message}",
                Result = 0
            }));
        }
    }
}