using Asp.Versioning;
using FluentValidation;
using Infrastructure.RefitClient;
using Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework;
using Refit;
using Services.Mapper;
using Services.Models.Request;
using Services.RefitClients.Interfaces;
using Services.Repositories.Interfaces;
using Services.Services.Implementations;
using Services.Services.Interfaces;
using Services.Validation;
using Services.Validation.Validators;
using WebApi.Mapper;
using WebApi.Settings;
using ExceptionHandlerMiddleware = WebApi.Middlewares.ExceptionHandlerMiddleware;

namespace WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataContext(this IServiceCollection services, 
        string connectionString)
    {
        services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddScoped<DbContext, DataContext>();
        
        return services;
    }

    public static IServiceCollection ConfigureRefitClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        var refitSettings = configuration.GetSection("RefitClientSettings")
            .Get<RefitClientSettings>();
        
        services.AddRefitClient<IOrderApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(refitSettings!.OrderApiUrl));

        services.AddScoped<IOrderApiClient, OrderApiClient>();

        return services;
    }
    
     public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
    
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddScoped<IValidator<DeleteReportModel>, DeleteReportValidator>();
        services.AddScoped<IValidator<GenerateReportModel>, GenerateReportValidator>();
        services.AddScoped<IValidator<GetAllReportsModel>, GetAllReportsValidator>();
        services.AddScoped<IValidator<GetReportByIdModel>, GetReportByIdValidator>();

        services.AddScoped<ReportValidator>();
        
        return services;
    }
    
    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(
            typeof(ServiceCostMappingProfile),
            typeof(ServiceReportMappingProfile),
            typeof(ServiceRevenueMappingProfile),
            typeof(ApiCostMappingProfile),
            typeof(ApiReportMappingProfile),
            typeof(ApiRevenueMappingProfile));
        
        return services;
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IReportService, ReportService>();

        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IReportRepository, ReportRepository>();

        return services;
    }
    
    
    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
    
    public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
    {
        services.AddTransient<ExceptionHandlerMiddleware>();
        
        return services;
    }
}