using DependencyInjection.Interfaces;
using DependencyInjection.Models;

namespace DependencyInjection.Services
{

    public class HDFCBankService : IBank
    {
        private readonly Guid _operationId;

        public HDFCBankService()
        {
            _operationId = Guid.NewGuid();
        }

        public string BankName => "HDFC Bank";

        public decimal GetInterestRate() => 6.8m; // Example interest rate

        public void ProcessSalary(IEmployee employee, decimal amount)
        {
            Console.WriteLine($"HDFC Bank processed salary of {amount} for {employee.Name}.");
        }

        public Guid GetOperationId() => _operationId;
    }
}
