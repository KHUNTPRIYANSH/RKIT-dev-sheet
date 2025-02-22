using Fleck;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using TPA_Server.Modals;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// JWT Authentication setup
var secretKey = "Priyansh Khunt's Secret-Key12345";
var key = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "your_app",
            ValidAudience = "your_users",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

// Enable authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// WebSocket setup
List<IWebSocketConnection> connectedClients = new();
var server = new WebSocketServer("ws://0.0.0.0:8181");
server.Start(socket =>
{
    socket.OnOpen = () =>
    {
        Console.WriteLine("✅ Client Connected!");
        connectedClients.Add(socket);
    };
    socket.OnMessage = message =>
    {
        Console.WriteLine("📩 Message received: " + message);
        if (message == "Success")
        {
            Console.WriteLine("✅ GUI confirmed token received!");
        }
    };
    socket.OnClose = () =>
    {
        Console.WriteLine("❌ Client Disconnected!");
        connectedClients.Remove(socket);
    };
});

// Login API
app.MapPost("/api/login", ([FromBody] LoginModel model) =>
{
    if (model.Username == "string" && model.Password == "string")
    {
        var token = GenerateJwtToken(model.Username);
        Console.WriteLine("🔑 JWT Token Generated: " + token);

        // Send token to GUI via WebSocket
        foreach (var client in connectedClients)
        {
            client.Send(token);
        }

        return Results.Ok(new { message = "Login successful, token sent to GUI.", token });
    }
    return Results.Unauthorized();
});

// Secure DTO Controller (Only Accessible by Authenticated Users)
app.MapGet("/api/dto", [Authorize] (ClaimsPrincipal user) =>
{
    var username = user.FindFirst(ClaimTypes.Name)?.Value;
    if (string.IsNullOrEmpty(username))
    {
        return Results.Unauthorized();
    }

    var userDto = GetUserDashboard(username);
    if (userDto == null)
    {
        return Results.NotFound(new { message = "User DTO not found." });
    }

    return Results.Ok(userDto);
});

// Generate JWT Token
string GenerateJwtToken(string username)
{
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    var token = new JwtSecurityToken(
        issuer: "your_app",
        audience: "your_users",
        claims: new[] { new Claim(ClaimTypes.Name, username) },
        expires: DateTime.UtcNow.AddHours(1),
        signingCredentials: creds
    );
    return new JwtSecurityTokenHandler().WriteToken(token);
}

// Get User Dashboard DTO
UserDashboardDTO? GetUserDashboard(string username)
{
    // Mock user roles for simplicity
    var userRoles = new Dictionary<string, string>
    {
        { "priyansh", "User" },
        { "romil", "Editor" },
        { "string", "Admin" }
    };

    if (!userRoles.ContainsKey(username))
    {
        return null;
    }

    var role = userRoles[username];
    string page = "/Pages/home.aspx";
    List<string> allowedSections = new List<string>();
    List<string> asyncScripts = new List<string>();
    List<string> sidebarOptions = new List<string>();

    if (role == "User")
    {
        page = "/Pages/home.aspx";
        allowedSections = new List<string> { "Profile", "Settings" };
        asyncScripts = new List<string> { "/scripts/dashboard.js" };
        sidebarOptions = new List<string> { "Home", "Settings" };
    }
    else if (role == "Editor")
    {
        page = "/Pages/editor-dashboard.aspx";
        allowedSections = new List<string> { "Posts", "Analytics" };
        asyncScripts = new List<string> { "/scripts/editor-tools.js" };
        sidebarOptions = new List<string> { "Dashboard", "Posts", "Analytics" };
    }
    else if (role == "Admin")
    {
        page = "/Pages/admin-dashboard.aspx";
        allowedSections = new List<string> { "Settings" };
        asyncScripts = new List<string> { "/scripts/admin-controls.js" };
        sidebarOptions = new List<string> { "Dashboard", "Users", "Settings", "Reports" };
    }

    return new UserDashboardDTO
    {
        Username = username,
        Role = role,
        Page = page,
        AllowedSections = allowedSections,
        AsyncScripts = asyncScripts,
        SidebarOptions = sidebarOptions,
        IsSetupComplete = false,
        LoadedModules = new List<string>(),
        UserPreference = ""
    };
}

app.Run();

record LoginModel(string Username, string Password);

class UserDashboardDTO
{
    public string Username { get; set; } = "";
    public string Role { get; set; } = "";
    public string Page { get; set; } = "";
    public List<string> AllowedSections { get; set; } = new();
    public List<string> AsyncScripts { get; set; } = new();
    public List<string> SidebarOptions { get; set; } = new();
    public bool IsSetupComplete { get; set; }
    public List<string> LoadedModules { get; set; } = new();
    public string UserPreference { get; set; } = "";
}
