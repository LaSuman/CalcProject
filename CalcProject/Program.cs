using CalculatorProject.Middleware;
using CalculatorProject.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.Converters.Add(
        new CalculatorRequestFlexibleConverter(
            builder.Services.BuildServiceProvider().GetRequiredService<ILogger<CalculatorRequestFlexibleConverter>>()
        )
    );
}).AddXmlSerializerFormatters();  // Added XML serializer formatters

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// This line is usually there by default, but make sure:
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//Added middleware to handle xml requests
app.UseMiddleware<XmlTagMappingMiddleware>();
app.MapControllers();

app.Run();
