using System.Diagnostics;

namespace UserManagementAPI.Middleware;

// Step 2: Copilot helped scaffold request/response logging
// - Logs HTTP method, path, and response status code
public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        Console.WriteLine($"[Request] {context.Request.Method} {context.Request.Path}");

        await _next(context);

        stopwatch.Stop();
        Console.WriteLine($"[Response] {context.Response.StatusCode} in {stopwatch.ElapsedMilliseconds}ms");
    }
}

// Extension method to keep Program.cs clean
public static class LoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingMiddleware>();
    }
}
