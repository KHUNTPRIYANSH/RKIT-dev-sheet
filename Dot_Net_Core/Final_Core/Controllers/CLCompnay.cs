using Final_Core.BL.Interfaces;
using Final_Core.BL.Operations;
using Final_Core.Filters;
using Final_Core.Models;
using Final_Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Final_Core.Controllers
{
    /// <summary>
    /// Controller for handling company-related API operations.
    /// Includes salary payments and email functionalities.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CLCompnay : ControllerBase
    {
        #region Fields

        private readonly ICompany _company;
        private readonly BLEmp01 _objBLEmployee;
        private readonly Response _objResponse;
        private readonly EmailService _emailService;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes the controller with company, employee, and email services.
        /// </summary>
        /// <param name="company">Company service for handling company-related operations.</param>
        /// <param name="objBLEmployee">Business logic layer for employee operations.</param>
        /// <param name="emailService">Service for handling email sending.</param>
        public CLCompnay(ICompany company, BLEmp01 objBLEmployee, EmailService emailService)
        {
            _company = company ?? throw new ArgumentNullException(nameof(company));
            _objBLEmployee = objBLEmployee ?? throw new ArgumentNullException(nameof(objBLEmployee));
            _objResponse = new Response();
            _emailService = emailService;
        }

        #endregion

        #region API Methods

        /// <summary>
        /// Processes salary payment for an employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee whose salary will be paid.</param>
        /// <returns>A response indicating success or failure of the payment.</returns>
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
                    Message = $"{employee.P01F02}'s salary transferred successfully in {_company.BankName} account : {employee.P01F08}rs. by {_company.CompanyName}",
                    Data = ""
                });
            }
            else
            {
                return Ok(new { IsError = true, Message = "Employee not found", Data = "" });
            }
        }

        #endregion

        #region Email Methods

        // The following method is commented out, demonstrating how email functionality could be added.

        /// <summary>
        /// Sends an email using the provided filter and request parameters.
        /// </summary>
        /// <param name="email">The recipient email address.</param>
        /// <param name="userName">The name of the user sending the email.</param>
        /// <param name="message">The content of the email message.</param>
        /// <returns>A response indicating the status of the email request.</returns>
        //[HttpPost("send")]
        //[ServiceFilter(typeof(SendEmailActionFilter))] // Applying the action filter here
        //public async Task<IActionResult> SendEmail(
        //    [FromQuery] string email = "default@example.com",  // Default email value
        //    [FromQuery] string userName = "Default User",      // Default username value
        //    [FromQuery] string message = "This is a default message.") // Default message value
        //{
        //    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(message))
        //    {
        //        return BadRequest(new { Message = "Email, username, and message are required." });
        //    }

        //    // You can proceed with other logic here if needed

        //    // Return OK response after action completion
        //    return Ok(new { Message = "Email request received and will be processed." });
        //}

        #endregion
    }
}
