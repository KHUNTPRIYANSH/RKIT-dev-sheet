using Final_Core.Models.POCO;

namespace Final_Core.BL.Interfaces
{
    /// <summary>
    /// Interface for company operations related to employee salary payments.
    /// </summary>
    public interface ICompany
    {
        /// <summary>
        /// Gets or sets the name of the company.
        /// This should be initialized in the implementing class.
        /// </summary>
        string CompanyName { get; set; }

        /// <summary>
        /// Gets the name of the bank associated with the company.
        /// </summary>
        string BankName { get; }

        /// <summary>
        /// Processes the salary payment for an employee.
        /// </summary>
        /// <param name="id">The ID of the employee whose salary is to be paid.</param>
        void PaySalary(int id);
    }
}
