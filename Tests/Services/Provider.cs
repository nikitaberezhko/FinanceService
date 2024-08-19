using Microsoft.Extensions.DependencyInjection;
using WebApi.Extensions;

namespace Tests.Services;

public static class Provider
{
    private static readonly IServiceProvider _serviceProvider;
    
    static Provider()
    {
        var services = new ServiceCollection();

        services.AddValidation();
        services.AddServices();
        services.AddMappers();
        
        _serviceProvider = services.BuildServiceProvider();
    }

    public static T Get<T>()
    {
        return _serviceProvider.GetRequiredService<T>();
    }
}