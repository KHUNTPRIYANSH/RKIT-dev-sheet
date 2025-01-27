using DependencyInjection.Interfaces;
using DependencyInjection.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompany _company;
        private readonly IEmployee _employee;

        public CompanyController(ICompany company, IEmployee employee)
        {
            _company = company;
            _employee = employee;
        }

        [HttpPost("pay-salary")]
        public IActionResult PaySalary(int employeeId, string employeeName, decimal amount)
        {
            _employee.EmployeeId = employeeId;
            _employee.Name = employeeName;

            _company.CompanyName = "TechCorp";
            _company.PaySalary(_employee, amount);

            return Ok(new { Message = "Salary processed successfully.", Employee = _employee.Name, Amount = amount });
        }

        [HttpGet("calculate-loan")]
        public IActionResult CalculateLoan(decimal principal, int tenureInMonths)
        {
            var repayment = _company.CalculateLoan(principal, tenureInMonths);
            return Ok(new { Message = "Loan calculated successfully.", Principal = principal, TenureInMonths = tenureInMonths, Repayment = repayment });
        }
    }
}
