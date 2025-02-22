using Fleck;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

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

        return Results.Ok(new { message = "Login successful, token sent to GUI." });
    }
    return Results.Unauthorized();
});

string GenerateJwtToken(string username)
{
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Priyansh Khunt's Secret-Key12345"));
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

app.Run();

record LoginModel(string Username, string Password);


