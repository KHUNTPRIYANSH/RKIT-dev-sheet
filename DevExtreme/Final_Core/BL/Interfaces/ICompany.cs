using Final_Core.Models.POCO;

namespace Final_Core.BL.Interfaces
{
    public interface ICompany
    {
        string CompanyName { get; set; } // Ensure it's initialized somewhere in implementation
        string BankName { get; }
        void PaySalary(int id);
    }
}
