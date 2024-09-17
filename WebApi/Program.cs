using SerilogTracing;
using WebApi.Extensions;
using WebApi.Middlewares;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        using var listener = new ActivityListenerConfiguration()
            .TraceToSharedLogger();

        services.AddControllers();
        
        // Extensions
        services.AddDataContext(builder.Configuration
            .GetConnectionString("DefaultConnectionString")!);
        services.ConfigureRefitClient(builder.Configuration);
        services.AddSwagger();
        services.AddValidation();
        services.AddMappers();
        services.AddServices();
        services.AddRepositories();
        services.AddVersioning();
        services.AddExceptionHandling();
        services.AddTelemetry();
        services.ConfigureSerilogAndZipkinTracing(builder.Configuration);
        
        
        var app = builder.Build();

        app.UseMiddleware<ExceptionHandlerMiddleware>();
        
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();
        
        app.MapPrometheusScrapingEndpoint();

        app.MapControllers();
        app.Run();
    }
}