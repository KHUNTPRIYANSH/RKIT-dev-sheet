using Final_Core.BL.Interfaces;
using Final_Core.BL.Operations;
using System;

namespace Final_Core.BL.Operations
{
    /// <summary>
    /// Represents a company that manages salary payments through a specified bank.
    /// </summary>
    public class Company : ICompany
    {
        #region Private Fields

        /// <summary>
        /// Bank service used for processing salary payments.
        /// </summary>
        private readonly IBank _bank;

        /// <summary>
        /// Employee business logic operations.
        /// </summary>
        private readonly BLEmp01 _objBLEmployee;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Company"/> class.
        /// </summary>
        /// <param name="bank">Bank service for processing salaries.</param>
        /// <param name="objBLEmployee">Employee business logic service.</param>
        /// <exception cref="ArgumentNullException">Thrown if any parameter is null.</exception>
        public Company(IBank bank, BLEmp01 objBLEmployee)
        {
            _bank = bank ?? throw new ArgumentNullException(nameof(bank));
            _objBLEmployee = objBLEmployee ?? throw new ArgumentNullException(nameof(objBLEmployee));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets the name of the bank used by the company.
        /// </summary>
        public string BankName => _bank.BankName;

        #endregion

        #region Public Methods

        /// <summary>
        /// Processes the salary payment for an employee by their ID.
        /// </summary>
        /// <param name="id">Employee ID.</param>
        public void PaySalary(int id)
        {
            var employee = _objBLEmployee.Get(id);
            if (employee != null)
            {
                _bank.ProcessSalary(employee, employee.P01F08);
                Console.WriteLine($"Salary of {employee.P01F08} paid to {employee.P01F02} through {BankName}.");
            }
            else
            {
                Console.WriteLine($"Error: Employee with ID {id} not found.");
            }
        }

        #endregion
    }
}
