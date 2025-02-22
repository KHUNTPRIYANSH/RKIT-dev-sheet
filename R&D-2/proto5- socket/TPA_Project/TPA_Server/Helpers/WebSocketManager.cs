using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace TPA_Server.Helpers
{
    public class WebSocketHandler
    {
        private static WebSocket _socket;

        public async Task HandleWebSocketConnection(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                _socket = await context.WebSockets.AcceptWebSocketAsync();
                Console.WriteLine("✅ Client Connected!");

                await ReceiveMessages(_socket);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }

        private async Task ReceiveMessages(WebSocket socket)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                    Console.WriteLine("❌ Client Disconnected!");
                }
                else
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Console.WriteLine($"🔹 Received from GUI: {message}");
                }
            }
        }

        public async Task SendTokenToGUI(string token)
        {
            if (_socket != null && _socket.State == WebSocketState.Open)
            {
                var tokenBytes = Encoding.UTF8.GetBytes(token);
                await _socket.SendAsync(new ArraySegment<byte>(tokenBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                Console.WriteLine($"✅ Sent Token: {token}");
            }
            else
            {
                Console.WriteLine("❌ WebSocket connection not established!");
            }
        }
    }

    public static class WebSocketExtensions
    {
        public static void UseWebSocketHandler(this IApplicationBuilder app)
        {
            app.UseWebSockets();
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/ws")
                {
                    var manager = context.RequestServices.GetRequiredService<WebSocketHandler>();
                    await manager.HandleWebSocketConnection(context);
                }
                else
                {
                    await next();
                }
            });
        }
    }
}
