using Final_Core.BL.Interfaces;
using Final_Core.Models.POCO;

namespace Final_Core.Services
{
    public class HDFCBankService : IBank
    {
        private readonly Guid _operationId;

        public HDFCBankService()
        {
            _operationId = Guid.NewGuid();
        }

        public string BankName => "HDFC Bank";

        public void ProcessSalary(Emp01 employee, decimal amount)
        {
            if (employee == null) // ✅ Null check for employee
            {
                Console.WriteLine("Error: Employee data is null.");
                return;
            }

            if (amount <= 0) // ✅ Salary validation
            {
                Console.WriteLine($"Error: Invalid salary amount ({amount}) for {employee.P01F02}.");
                return;
            }

            Console.WriteLine($"HDFC Bank processed salary of {amount} for {employee.P01F02}");
        }

        public Guid GetOperationId() => _operationId;
    }
}
