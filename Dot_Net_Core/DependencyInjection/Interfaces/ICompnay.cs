using DependencyInjection.Models;

namespace DependencyInjection.Interfaces
{
    public interface ICompany
    {
        string CompanyName { get; set; }
        void PaySalary(IEmployee employee, decimal amount);
        decimal CalculateLoan(decimal principal, int tenureInMonths);
    }

}
