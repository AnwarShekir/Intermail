using System.Net;
using Intermail.Dto;
using Intermail.Middleware;
using Intermail.Services;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAppLogger, AppLogger>();
builder.Services.AddSingleton<IAppConfiguration, AppConfiguration>();



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


