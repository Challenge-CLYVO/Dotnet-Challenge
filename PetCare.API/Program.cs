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
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using PetCare.API.Metrics;
using System.Diagnostics.Metrics;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", "PetCare.API")
    .WriteTo.Console()
    .WriteTo.File(
        "logs/petcare-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

const string serviceName = "PetCare.API";
const string serviceVersion = "1.0.0";

var meter = new Meter("PetCare.API.Metrics", serviceVersion);
var errorCounter = meter.CreateCounter<long>(
    "petcare_http_errors_total",
    description: "Quantidade total de erros HTTP da aplicação.");
builder.Host.UseSerilog();

builder.Services
    .AddOpenTelemetry()
    .WithTracing(tracing =>
    {
        tracing
            .SetResourceBuilder(
                ResourceBuilder.CreateDefault()
                    .AddService(
                        serviceName,
                        serviceVersion: serviceVersion))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddEntityFrameworkCoreInstrumentation()
            .AddConsoleExporter();
    })
    .WithMetrics(metrics =>
    {
        metrics
            .SetResourceBuilder(
                ResourceBuilder.CreateDefault()
                    .AddService(
                        serviceName,
                        serviceVersion: serviceVersion))
            .AddMeter(PetCareMetrics.MeterName)
            .AddMeter("PetCare.API.Metrics")
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRuntimeInstrumentation()
            .AddPrometheusExporter();
    });

var connectionString = builder.Configuration.GetConnectionString("RecommendaContextOracle");

if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Connection string 'RecommendaContextOracle' não configurada.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(connectionString));

// Substitua o registro antigo por este:
builder.Services.AddAutoMapper(cfg => { }, typeof(TutorProfile));



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

app.MapPrometheusScrapingEndpoint();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

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

public partial class Program
{
}
