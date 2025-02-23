using Fleck;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using TPA_Server.Helpers;
using TPA_Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JsonSerializerOptions>(options =>
{
    options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});
// Configure CORS
builder.Services.AddCors(options =>
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()));

// Configure JWT
JwtConfig.ConfigureAuthentication(builder.Services);

// Register services
builder.Services.AddSingleton<TPA_Server.Services.WebSocketManager>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Middleware pipeline
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseAuthentication();
app.UseAuthorization();

// Initialize WebSocket
var webSocketManager = app.Services.GetRequiredService<TPA_Server.Services.WebSocketManager>();
webSocketManager.InitializeWebSocketServer(app.Services);

app.MapControllers();

app.Run();