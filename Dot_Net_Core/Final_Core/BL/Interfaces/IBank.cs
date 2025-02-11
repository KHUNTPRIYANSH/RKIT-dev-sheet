using Final_Core.Models.POCO;

namespace Final_Core.BL.Interfaces
{
    public interface IBank
    {
        string BankName { get; }
        void ProcessSalary(Emp01 employee, decimal amount);
        Guid GetOperationId(); // Used to demonstrate service lifetime tracking
    }
}
