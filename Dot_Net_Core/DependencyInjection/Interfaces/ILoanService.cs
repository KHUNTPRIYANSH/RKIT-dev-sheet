namespace DependencyInjection.Interfaces
{
    public interface ILoanService
    {
        decimal CalculateLoanRepayment(decimal principal, int tenureInMonths);
    }
}
