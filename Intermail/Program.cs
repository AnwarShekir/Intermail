using System.Net;
using Intermail.Dto;
using Intermail.Middleware;
using Intermail.Services;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

Log.Logger = new LoggerConfiguration().WriteTo.File("/logs/apploggs.txt", LogEventLevel.Information).CreateLogger();
builder.Host.UseSerilog();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IAppConfiguration, AppConfiguration>();
builder.Services.AddScoped<IAppLogger, AppLogger>();
builder.Services.AddScoped<IExternalService, ExternalService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler(new ExceptionHandlerOptions
{
    ExceptionHandler = ApplicationExceptionHandlerMiddleware.HandleException
});

app.UseMiddleware<AuthMiddleware>();


app.MapPost("/LoyaltyPoint", async (RequestDto dto, IExternalService externalService) =>
{
    await externalService.SendLoyaltyPoint(dto);
    return Results.Ok();
});



app.Run();


