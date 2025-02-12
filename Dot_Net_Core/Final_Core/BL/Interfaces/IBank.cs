using Final_Core.Models.POCO;

namespace Final_Core.BL.Interfaces
{
    /// <summary>
    /// Interface for Bank operations related to employee salary processing.
    /// </summary>
    public interface IBank
    {
        /// <summary>
        /// Gets the name of the bank.
        /// </summary>
        string BankName { get; }

        /// <summary>
        /// Processes the salary for an employee.
        /// </summary>
        /// <param name="employee">The employee whose salary is to be processed.</param>
        /// <param name="amount">The salary amount to be processed.</param>
        void ProcessSalary(Emp01 employee, decimal amount);

        /// <summary>
        /// Gets the operation ID for the salary processing transaction.
        /// Used for service lifetime tracking.
        /// </summary>
        /// <returns>A unique operation ID.</returns>
        Guid GetOperationId();
    }
}
