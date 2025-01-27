using DependencyInjection.Interfaces;
using DependencyInjection.Models;

namespace DependencyInjection.Services
{
    public class ICICIBankService : IBank
    {
        private readonly Guid _operationId;

        public ICICIBankService()
        {
            _operationId = Guid.NewGuid();
        }

        public string BankName => "ICICI Bank";

        public decimal GetInterestRate() => 7.5m; // Example interest rate

        public void ProcessSalary(IEmployee employee, decimal amount)
        {
            Console.WriteLine($"ICICI Bank processed salary of {amount} for {employee.Name}.");
        }

        public Guid GetOperationId() => _operationId;
    }

}
