using UserManagementAPI;
using UserManagementAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();

var app = builder.Build();

app.UseGlobalErrorHandling();
app.UseTokenAuthentication();
app.UseRequestResponseLogging();

// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();