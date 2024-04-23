using AppointmentScheduler.Infrasturcture;
using AppointmentScheduler.Core;
using AppointmentScheduler.Server.Middleware;
using System.Diagnostics;
using AppointmentScheduler.Core.Domain.IdentityEntities;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureExtensions(builder.Configuration);
builder.Services.ConfigureInfra(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<MeasureRequestProcessingMiddleware>();
builder.Services.AddSingleton<ExceptionHandler>();

var app = builder.Build();
app.UseMiddleware<MeasureRequestProcessingMiddleware>();
app.UseMiddleware<ExceptionHandler>();
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHsts();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
