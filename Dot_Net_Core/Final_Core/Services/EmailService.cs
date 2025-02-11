using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Final_Core.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<EmailService> _logger;
        private readonly HttpClient _httpClient;

        public EmailService(IConfiguration config, ILogger<EmailService> logger, HttpClient httpClient)
        {
            _config = config;
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task SendEmailAsync(string fromName, string toEmail, string userName, string message)
        {
            try
            {
                // Get EmailJS credentials from appsettings.json
                string serviceId = _config["EmailJS:ServiceID"];
                string templateId = _config["EmailJS:TemplateID"];
                string userId = _config["EmailJS:UserID"];

                // Prepare the request payload
                var payload = new
                {
                    service_id = serviceId,
                    template_id = templateId,
                    user_id = userId,
                    template_params = new
                    {
                        user_name = userName,    // Sender's name
                        user_email = toEmail,    // Recipient email address
                        message = message        // Message content
                    }
                };

                // Serialize the payload to JSON
                var jsonPayload = JsonSerializer.Serialize(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                // Send the request to EmailJS
                var response = await _httpClient.PostAsync("https://api.emailjs.com/api/v1.0/email/send", content);

                // Check if the email was sent successfully
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Email successfully sent to {toEmail}");
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Failed to send email to {toEmail}. Response: {errorResponse}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email.");
            }
        }
    }
}
