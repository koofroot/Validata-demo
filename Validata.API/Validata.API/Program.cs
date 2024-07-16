using Validata.API.Middlewares;
using Validata.Data.Extensions;
using Validata.Infrastructure;
using FluentValidation.AspNetCore;
using Validata.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddContext(connectionString);
builder.Services.AddData();
builder.Services.AddInfrastructure();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddModelValidation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseMiddleware<ServerErrorMiddleware>();

app.MapControllers();

app.Run();
