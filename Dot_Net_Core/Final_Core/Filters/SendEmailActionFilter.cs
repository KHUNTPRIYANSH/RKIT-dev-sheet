using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Final_Core.Services;
using System.Threading.Tasks;

namespace Final_Core.Filters
{
    public class SendEmailActionFilter : IAsyncActionFilter
    {
        private readonly EmailService _emailService;

        public SendEmailActionFilter(EmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Proceed with the action execution
            var resultContext = await next();

            // After the action is executed, send the email
            if (resultContext.Result is OkObjectResult || resultContext.Result is StatusCodeResult)
            {
                // You can fetch the email details from the context or other sources
                var email = context.HttpContext.Request.Query["email"].ToString();
                var userName = context.HttpContext.Request.Query["userName"].ToString();
                var message = context.HttpContext.Request.Query["message"].ToString();

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(message))
                {
                    // Send email after action is performed
                    await _emailService.SendEmailAsync(userName, email, userName, message);
                }
            }
        }
    }
}
