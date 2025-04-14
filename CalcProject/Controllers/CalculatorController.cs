using CalculatorProject.Models;
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

                return Ok();
            }

              
            catch (Exception e)
            {
                 return BadRequest(e.Message);
            }
          
        }


        [HttpPost]
        [Route("CalculateXML")]
        public IActionResult CalculateXML([FromBody] CalculatorRequest request)
        {
            try
            {

                return Ok();
            }


            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

    }
}
