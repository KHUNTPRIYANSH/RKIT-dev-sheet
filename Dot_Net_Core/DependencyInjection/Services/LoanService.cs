using DependencyInjection.Interfaces;

namespace DependencyInjection.Services
{
    public class LoanService : ILoanService
    {
        private readonly IBank _bank;

        public LoanService(IBank bank)
        {
            _bank = bank;
        }

        public decimal CalculateLoanRepayment(decimal principal, int tenureInMonths)
        {
            var interestRate = _bank.GetInterestRate();
            var monthlyRate = interestRate / 12 / 100;
            return principal * (1 + monthlyRate * tenureInMonths);
        }
    }

}
