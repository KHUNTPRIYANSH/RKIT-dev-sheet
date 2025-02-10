using Final_Core.BL.Interfaces;
using Final_Core.BL.Operations;
using Final_Core.Models;
using Final_Core.Models.POCO;
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

        public CLCompnay(ICompany company, BLEmp01 objBLEmployee)
        {
            _company = company ?? throw new ArgumentNullException(nameof(company));
            _objBLEmployee = objBLEmployee ?? throw new ArgumentNullException(nameof(objBLEmployee));
            _objResponse = new Response();
        }

        /// <summary>
        /// Pays salary to an employee by their ID.
        /// </summary>
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
                    Message = $"{employee.P01F02}'s salary transfered successfully in your {_company.BankName} account : {employee.P01F08}rs.",
                    Data = ""
                });
            }
            else
            {
                return Ok(new {IsError = true, Message = "Employee not found", Data = "" });
            }
        }


    }
}
