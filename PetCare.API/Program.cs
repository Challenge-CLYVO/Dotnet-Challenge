using Microsoft.EntityFrameworkCore;
using PetCare.Infrastructure.Data;
using PetCare.API.Middlewares;
using PetCare.Application.Interfaces;
using PetCare.Application.Services;
using PetCare.Application.Mappings;
using PetCare.Infrastructure.Repositories;
using System.Reflection;
using PetCare.API.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", "PetCare.API")
    .WriteTo.Console()
    .WriteTo.File(
        "logs/petcare-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Connection String
var connectionString = builder.Configuration.GetConnectionString("RecommendaContextOracle");

if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Connection string 'RecommendaContextOracle' não configurada.");
}

// Oracle
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(connectionString));

// AutoMapper
builder.Services.AddAutoMapper(typeof(TutorProfile));


// Repositories
builder.Services.AddScoped<IAplicacaoVacinaRepository, AplicacaoVacinaRepository>();
builder.Services.AddScoped<IHistoricoSaudeRepository, HistoricoSaudeRepository>();
builder.Services.AddScoped<IConsultaRepository, ConsultaRepository>();
builder.Services.AddScoped<IClinicaRepository, ClinicaRepository>();
builder.Services.AddScoped<IVacinaRepository, VacinaRepository>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<ITutorRepository, TutorRepository>();

// Services
builder.Services.AddScoped<IAplicacaoVacinaService, AplicacaoVacinaService>();
builder.Services.AddScoped<IHistoricoSaudeService, HistoricoSaudeService>();
builder.Services.AddScoped<IConsultaService, ConsultaService>();
builder.Services.AddScoped<IClinicaService, ClinicaService>();
builder.Services.AddScoped<IVacinaService, VacinaService>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<ITutorService, TutorService>();

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddHttpClient();

// Health Checks
builder.Services
    .AddHealthChecks()
    .AddCheck<ApiHealthCheck>("api")
    .AddDbContextCheck<AppDbContext>(
        "oracle",
        tags: new[] { "db", "oracle" })
    .AddCheck<ExternalServiceHealthCheck>(
        "external-service");

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseMiddleware<CorrelationIdMiddleware>();

app.UseMiddleware<ExceptionMiddleware>();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";

        var response = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(entry => new
            {
                name = entry.Key,
                status = entry.Value.Status.ToString(),
                description = entry.Value.Description
            }),
            totalDuration = report.TotalDuration
        };

        await context.Response.WriteAsJsonAsync(response);
    }
});

app.Run();