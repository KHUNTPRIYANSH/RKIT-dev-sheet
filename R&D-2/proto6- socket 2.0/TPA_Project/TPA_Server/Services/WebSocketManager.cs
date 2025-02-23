using Fleck;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using TPA_Server.Modals;
using System.Text.Json;

namespace TPA_Server.Services
{

    public sealed class WebSocketManager
    {
        private readonly List<IWebSocketConnection> _connectedClients = new();
        private readonly ConcurrentDictionary<string, TaskCompletionSource<UserDashboardDTO>> _pendingTokens = new(); private readonly ILogger<WebSocketManager> _logger;

        public WebSocketManager(ILogger<WebSocketManager> logger)
        {
            _logger = logger;
        }

        public void InitializeWebSocketServer(IServiceProvider serviceProvider)
        {
            var server = new WebSocketServer("ws://0.0.0.0:8181");
            server.Start(socket =>
            {
                socket.OnOpen = () => HandleConnectionOpen(socket);
                socket.OnMessage = message => HandleMessage(message, socket);
                socket.OnClose = () => HandleConnectionClose(socket);
            });
        }

        private void HandleConnectionOpen(IWebSocketConnection socket)
        {
            _logger.LogInformation("Client connected: {ClientId}", socket.ConnectionInfo.Id);
            _connectedClients.Add(socket);
        }

        private void HandleMessage(string message, IWebSocketConnection socket)
        {
            _logger.LogInformation("Message from {ClientId}: {Message}",
                socket.ConnectionInfo.Id, message);

            if (message.StartsWith("DTO_CONFIRMED:"))
            {
                try
                {
                    // Split the message into token and DTO parts
                    var parts = message.Split(new[] { ':' }, 3); // Split into 3 parts: prefix, token, and DTO JSON
                    if (parts.Length < 3)
                    {
                        _logger.LogError("Invalid DTO_CONFIRMED message format: {Message}", message);
                        return;
                    }

                    var token = parts[1]; // Token is the second part
                    var dtoJson = parts[2]; // DTO JSON is the third part

                    // Deserialize the DTO
                    var dto = JsonSerializer.Deserialize<UserDashboardDTO>(dtoJson);

                    // Complete the pending task
                    if (_pendingTokens.TryRemove(token, out var tcs))
                    {
                        tcs.TrySetResult(dto);
                        _logger.LogInformation("DTO confirmed for token: {Token}", token);
                    }
                    else
                    {
                        _logger.LogWarning("No pending token found for: {Token}", token);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing DTO confirmation");
                }
            }
        }

        private void HandleConnectionClose(IWebSocketConnection socket)
        {
            _logger.LogInformation("Client disconnected: {ClientId}", socket.ConnectionInfo.Id);
            _connectedClients.Remove(socket);
        }

        public void AddPendingToken(string token, TaskCompletionSource<UserDashboardDTO> tcs)
        {
            _pendingTokens.TryAdd(token, tcs);
        }

        public void SendTokenToClients(string message)
        {
            foreach (var client in _connectedClients.ToList())
            {
                try
                {
                    client.Send(message);
                    _logger.LogInformation("Message sent to {ClientId}: {Message}",
                        client.ConnectionInfo.Id, message);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex, "Error sending message to {ClientId}",
                        client.ConnectionInfo.Id);
                }
            }
        }

        public void RemovePendingToken(string token)
        {
            _pendingTokens.TryRemove(token, out _);
        }
    }
}
