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
                _ => throw new ArgumentOutOfRangeException()
            };
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }


    [HttpPost()]
    [Route("CalculateXML")]
    [Consumes("application/xml")]
    public async Task<IActionResult> CalculateXML([FromBody] CalculatorRequestXml xmlRequest)
    {
        var request = xmlRequest.ToCalculatorRequest();
        var op = request.Maths?.Operation;
        var result = op.ID switch
        {
            nameof(Operator.Plus) => new AddService().Calculate(request),
            nameof(Operator.Subtraction) => new SubService().Calculate(request),
            nameof(Operator.Multiplication) => new MulService().Calculate(request),
            nameof(Operator.Division) => new DivService().Calculate(request),
            _ => throw new ArgumentOutOfRangeException()
        };
        return Ok(result);
    }
}
