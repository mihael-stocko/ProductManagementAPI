public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try {
            await next(context);
        }
        catch (Exception ex) {
            logger.LogError(ex, "An unhandled exception occurred.");
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new { Error = "Internal Server Error" });
        }
    }
}