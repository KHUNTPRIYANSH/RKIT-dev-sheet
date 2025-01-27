using DependencyInjection.Interfaces;

namespace DependencyInjection.Models
{
    public class Company : ICompany
    {
        private readonly IBank _bank;
        private readonly ILoanService _loanService;

        public Company(IBank bank, ILoanService loanService)
        {
            _bank = bank;
            _loanService = loanService;
        }

        public string CompanyName { get; set; }

        public void PaySalary(IEmployee employee, decimal amount)
        {
            _bank.ProcessSalary(employee, amount);
            Console.WriteLine($"Salary of {amount} paid to {employee.Name} through {_bank.BankName}.");
        }

        public decimal CalculateLoan(decimal principal, int tenureInMonths)
        {
            return _loanService.CalculateLoanRepayment(principal, tenureInMonths);
        }
    }
}
