using System.Net;

namespace UserManagementAPI.Middleware;

// Step 4: Copilot implemented token-based authentication
// - Validates `Authorization: Bearer <token>` header
// - Rejects requests with 401 Unauthorized if invalid
public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    private const string VALID_TOKEN = "dummy-token"; // this is dummy token for demo purposes!!

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("Authorization", out var authHeader) ||
            !authHeader.ToString().StartsWith("Bearer ") ||
            authHeader.ToString().Substring("Bearer ".Length) != VALID_TOKEN)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        await _next(context);
    }
}

public static class AuthenticationMiddlewareExtensions
{
    public static IApplicationBuilder UseTokenAuthentication(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthenticationMiddleware>();
    }
}
