using System.Text;

namespace CalculatorProject.Middleware;


public class XmlTagMappingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/api/Calculator/CalculateXML", StringComparison.OrdinalIgnoreCase) &&
            context.Request.ContentType?.Contains("application/xml") == true)
        {
            context.Request.EnableBuffering();

            using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
            var xmlBody = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0; // Reset the stream position for further processing 

            // Replace tags to match the expected model
            var fixedXml = xmlBody
                .Replace("<Maths>", "<MyMaths>")
                .Replace("</Maths>", "</MyMaths>")
                .Replace("<Operation", "<MyOperation")
                .Replace("</Operation>", "</MyOperation>");

            // Replace body
            var newBodyBytes = Encoding.UTF8.GetBytes(fixedXml);
            context.Request.Body = new MemoryStream(newBodyBytes);
            context.Request.ContentLength = newBodyBytes.Length;
        }

        await next(context);
    }
}
