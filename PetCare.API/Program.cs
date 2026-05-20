using Microsoft.EntityFrameworkCore;
using PetCare.Infrastructure.Data;

using PetCare.Application.Interfaces;
using PetCare.Application.Services;

using PetCare.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Connection String
var connectionString = builder.Configuration.GetConnectionString("RecommendaContextOracle");

if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Connection string 'RecommendaContextOracle' não configurada.");
}

// Oracle
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(connectionString));

// Repositories
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<ITutorRepository, TutorRepository>();

// Services
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<ITutorService, TutorService>();

// Controllers + Swagger
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();