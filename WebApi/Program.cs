using Infrastructure.RefitClient;
using Refit;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        builder.Services.AddRefitClient<IOrderApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:8081"));
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        
            app.UseSwagger();
            app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthorization();
        

        app.Run();
    }
}