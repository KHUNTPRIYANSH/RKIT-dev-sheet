using Fleck;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using TPA_Server.Modals;
using System.Text.Json;

namespace TPA_Server.Services
{
    /// <summary>
    /// Manages WebSocket connections and communication between the server and clients.
    /// </summary>
    public sealed class WebSocketManager
    {
        #region Fields
        private readonly List<IWebSocketConnection> _connectedClients = new();
        private readonly ConcurrentDictionary<string, TaskCompletionSource<UserDashboardDTO>> _pendingTokens = new();
        private readonly ILogger<WebSocketManager> _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="WebSocketManager"/> class.
        /// </summary>
        /// <param name="logger">Logger for capturing runtime events and errors.</param>
        public WebSocketManager(ILogger<WebSocketManager> logger)
        {
            _logger = logger;
        }
        #endregion

        #region WebSocket Server Initialization
        /// <summary>
        /// Initializes the WebSocket server and sets up event handlers.
        /// </summary>
        /// <param name="serviceProvider">The service provider for dependency injection.</param>
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
        #endregion

        #region Event Handlers
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
        #endregion

        #region Token Management
        /// <summary>
        /// Adds a pending token to await DTO confirmation.
        /// </summary>
        /// <param name="token">The authentication token.</param>
        /// <param name="tcs">The task completion source to resolve when DTO is confirmed.</param>
        public void AddPendingToken(string token, TaskCompletionSource<UserDashboardDTO> tcs)
        {
            _pendingTokens.TryAdd(token, tcs);
        }

        /// <summary>
        /// Sends a message to all connected WebSocket clients.
        /// </summary>
        /// <param name="message">The message to send.</param>
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

        /// <summary>
        /// Removes a pending token from the tracking dictionary.
        /// </summary>
        /// <param name="token">The authentication token to remove.</param>
        public void RemovePendingToken(string token)
        {
            _pendingTokens.TryRemove(token, out _);
        }
        #endregion
    }
}