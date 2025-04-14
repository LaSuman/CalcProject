using CalculatorProject.Models;
using CalculatorProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        [HttpPost]
        [Route("CalculateJSON")]
        public IActionResult CalculateJSON([FromBody] CalculatorRequest request)
        {
            try
            {
                var result = request.Maths?.Operation?.ID switch
                {
                    nameof(Operator.Plus) => new AddService().Calculate(request),
                    nameof(Operator.Subtraction) => new SubService().Calculate(request),
                    nameof(Operator.Multiplication) => new MulService().Calculate(request),
                    nameof(Operator.Division) => new DivService().Calculate(request),

                    _ => throw new InvalidOperationException("Invalid operation")
                };

                return Ok();
            }

              
            catch (Exception e)
            {
                 return BadRequest(e.Message);
            }
          
        }


        [HttpPost]
        [Route("CalculateXML")]
        [Consumes("application/xml")]
        public IActionResult CalculateXML(CalculatorRequestXml xml)
        {
            try
            {
                var request = xml.ToCalculatorRequest();

                var result = request.Maths?.Operation?.ID switch
                {
                    nameof(Operator.Plus) => new AddService().Calculate(request),
                    nameof(Operator.Subtraction) => new SubService().Calculate(request),
                    nameof(Operator.Multiplication) => new MulService().Calculate(request),
                    nameof(Operator.Division) => new DivService().Calculate(request),

                    _ => throw new InvalidOperationException("Invalid operation")
                };

                return Ok();
            }


            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
