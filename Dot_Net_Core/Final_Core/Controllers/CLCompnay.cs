using Final_Core.BL.Interfaces;
using Final_Core.BL.Operations;
using Final_Core.Filters;
using Final_Core.Models;
using Final_Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Final_Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CLCompnay : ControllerBase
    {
        private readonly ICompany _company;
        private readonly BLEmp01 _objBLEmployee;
        private readonly Response _objResponse;
        private readonly EmailService _emailService;

        public CLCompnay(ICompany company, BLEmp01 objBLEmployee, EmailService emailService)
        {
            _company = company ?? throw new ArgumentNullException(nameof(company));
            _objBLEmployee = objBLEmployee ?? throw new ArgumentNullException(nameof(objBLEmployee));
            _objResponse = new Response();
            _emailService = emailService;
        }

        [HttpPost("pay_salary")]
        public IActionResult PaySalary(int id)
        {
            var employee = _objBLEmployee.Get(id);
            if (employee != null)
            {
                _company.CompanyName = "RKIT Software pvt. ltd.";
                _company.PaySalary(employee.P01F01);

                return Ok(new
                {
                    IsError = false,
                    Message = $"{employee.P01F02}'s salary transferred successfully in your {_company.BankName} account : {employee.P01F08}rs.",
                    Data = ""
                });
            }
            else
            {
                return Ok(new { IsError = true, Message = "Employee not found", Data = "" });
            }
        }

        // Send Email using the filter
        [HttpPost("send")]
        [ServiceFilter(typeof(SendEmailActionFilter))] // Applying the action filter here
        public async Task<IActionResult> SendEmail(
          [FromQuery] string email = "default@example.com",  // Default email value
          [FromQuery] string userName = "Default User",      // Default username value
          [FromQuery] string message = "This is a default message.") // Default message value
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(message))
            {
                return BadRequest(new { Message = "Email, username, and message are required." });
            }

            // You can proceed with other logic here if needed

            // Return OK response after action completion
            return Ok(new { Message = "Email request received and will be processed." });
        }
    }
}
