using Fleck;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using TPA_Server.Helpers;
using TPA_Server.Services;

var builder = WebApplication.CreateBuilder(args);

#region Logging Configuration
// Configure logging to use console and debug outputs
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
#endregion

#region Service Configuration
// Add controllers, API explorer, and Swagger for API documentation
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure JSON serialization to use camelCase naming
builder.Services.Configure<JsonSerializerOptions>(options =>
{
    options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});
#endregion

#region CORS Configuration
// Configure CORS to allow requests from any origin, method, and header
builder.Services.AddCors(options =>
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()));
#endregion

#region JWT Authentication Configuration
// Configure JWT authentication using the helper method
JwtConfig.ConfigureAuthentication(builder.Services);
#endregion

#region Dependency Injection
// Register services for dependency injection
builder.Services.AddSingleton<TPA_Server.Services.WebSocketManager>();
builder.Services.AddScoped<UserService>();
#endregion

var app = builder.Build();

#region Middleware Pipeline
// Enable CORS for all requests
app.UseCors("AllowAll");

// Enable Swagger and developer exception page in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();
#endregion

#region WebSocket Initialization
// Initialize WebSocket server
var webSocketManager = app.Services.GetRequiredService<TPA_Server.Services.WebSocketManager>();
webSocketManager.InitializeWebSocketServer(app.Services);
#endregion

#region Endpoint Mapping
// Map controllers to handle API requests
app.MapControllers();
#endregion

// Start the application
app.Run();