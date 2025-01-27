using DependencyInjection.Models;

namespace DependencyInjection.Interfaces
{
    public interface IBank
    {
        string BankName { get; }
        decimal GetInterestRate(); // Bank-specific interest rate
        void ProcessSalary(IEmployee employee, decimal amount);
        Guid GetOperationId(); // To demonstrate service lifetime
    }

}
