using Final_Core.BL.Interfaces;
using Final_Core.Models.POCO;
using System;

namespace Final_Core.Services
{
    /// <summary>
    /// Implementation of for ICICI Bank salary processing.
    /// </summary>
    public class ICICIBankService : IBank
    {
        #region Private Fields

        /// <summary>
        /// Unique operation identifier for the service instance.
        /// </summary>
        private readonly Guid _operationId;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ICICIBankService"/> class.
        /// </summary>
        public ICICIBankService()
        {
            _operationId = Guid.NewGuid();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the bank name.
        /// </summary>
        public string BankName => "ICICI Bank";

        #endregion

        #region Public Methods

        /// <summary>
        /// Processes the salary payment for an employee.
        /// </summary>
        /// <param name="employee">Employee for whom salary is being processed.</param>
        /// <param name="amount">Salary amount to be processed.</param>
        public void ProcessSalary(Emp01 employee, decimal amount)
        {
            if (employee == null) //  Null check for employee
            {
                Console.WriteLine("Error: Employee data is null.");
                return;
            }

            if (amount <= 0) //  Salary validation
            {
                Console.WriteLine($"Error: Invalid salary amount ({amount}) for {employee.P01F02}.");
                return;
            }

            Console.WriteLine($"ICICI Bank processed salary of {amount} for {employee.P01F02}");
        }

        /// <summary>
        /// Gets the unique operation identifier for the service instance.
        /// </summary>
        /// <returns>Operation ID as a <see cref="Guid"/>.</returns>
        public Guid GetOperationId() => _operationId;

        #endregion
    }
}
