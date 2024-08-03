namespace WebApi.Middlewares;

public class ExceptionHandlerMiddleware : IMiddleware
{
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        throw new NotImplementedException();
    }
}