using Microsoft.EntityFrameworkCore;
using PetCare.Infrastructure.Data;
using PetCare.Application.Interfaces;
using PetCare.Application.Services;
using PetCare.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 🔌 Banco Oracle
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleDb")));

// 🔗 Dependências
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IPetService, PetService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();