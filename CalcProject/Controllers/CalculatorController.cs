using CalculatorProject.Models;
using CalculatorProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CalculatorController : ControllerBase
{
    [HttpPost()]
    [Route("CalculateJSON")]
    [Produces("application/json")]
    public IActionResult CalculateJSON([FromBody] CalculatorRequest request)
    {
        try
        {
            var op = request.Maths?.Operation;
            var result = op.ID switch
            {
                nameof(Operator.Plus) => new AddService().Calculate(request),
                nameof(Operator.Subtraction) => new SubService().Calculate(request),
                nameof(Operator.Multiplication) => new MulService().Calculate(request),
                nameof(Operator.Division) => new DivService().Calculate(request),
                nameof(Operator.Exponential) => new ExpService().Calculate(request),
                _ => throw new ArgumentOutOfRangeException()
            };
            return Ok(new CustomResponse
            { 
                Success = true,
                Message = $"Operation successful.",
                Result = result
            });
        }
        catch (Exception e)
        {
            return BadRequest(new CustomResponse
            {
                Success = false,
                Message = "Invalid operation request.",
                Result = 0
            });
        }
    }


    [HttpPost()]
    [Route("CalculateXML")]
    [Consumes("application/xml")]
    [Produces("application/xml")]
    public async Task<IActionResult> CalculateXML([FromBody] CalculatorRequestXml xmlRequest)
    {
        try
        {
            var request = xmlRequest.ToCalculatorRequest();
            var op = request.Maths?.Operation;
            var result = op.ID switch
            {
                nameof(Operator.Plus) => new AddService().Calculate(request),
                nameof(Operator.Subtraction) => new SubService().Calculate(request),
                nameof(Operator.Multiplication) => new MulService().Calculate(request),
                nameof(Operator.Division) => new DivService().Calculate(request),
                nameof(Operator.Exponential) => new ExpService().Calculate(request),
                _ => throw new ArgumentOutOfRangeException()
            };
            return Ok(new CustomResponse
            {
                Success = true,
                Message = $"Operation successful.",
                Result = result
            });
        }
        catch (Exception e)
        {
            return BadRequest(new CustomResponse
            {
                Success = false,
                Message = "Invalid operation request.",
                Result = 0
            });
        }
    }
}