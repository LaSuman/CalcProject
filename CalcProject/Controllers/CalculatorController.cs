using CalculatorProject.Models;
using CalculatorProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;

namespace CalculatorProject.Controllers
{
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
                var result = request.Maths.Operation.ID switch
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
           //try
            //{
                //using (var reader = new StreamReader(Request.Body))
               // {
                    //var xmlString = await reader.ReadToEndAsync();

                    //// Normalize the XML to use "MyMaths" and "MyOperation"
                    //xmlString = xmlString.Replace("<Maths>", "<MyMaths>").Replace("</Maths>", "</MyMaths>");
                    //xmlString = xmlString.Replace("<Operation ", "<MyOperation ")
                    //    .Replace("<Operation>", "<MyOperation>")
                    //    .Replace("</Operation>", "</MyOperation>");

                    //var serializer = new XmlSerializer(typeof(CalculatorRequestXml));
                    //using (var stringReader = new StringReader(xmlString))
                    //{
                    //    var xmlRequest = (CalculatorRequestXml)serializer.Deserialize(stringReader);
                        var request = xmlRequest.ToCalculatorRequest();

                        var result = request.Maths.Operation.ID switch
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
            //}
            //catch (Exception e)
            //{
            //    return BadRequest();
            //}
       // }
   // }
}
