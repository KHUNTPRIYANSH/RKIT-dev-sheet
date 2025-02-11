using Final_Core.BL.Interfaces;
using Final_Core.BL.Operations;

public class Company : ICompany
{
    private readonly IBank _bank;
    private readonly BLEmp01 _objBLEmployee;

    public Company(IBank bank, BLEmp01 objBLEmployee)
    {
        _bank = bank ?? throw new ArgumentNullException(nameof(bank));
        _objBLEmployee = objBLEmployee ?? throw new ArgumentNullException(nameof(objBLEmployee));
    }

    public string CompanyName { get; set; }
    public string BankName => _bank.BankName;  // Implement BankName property

    public void PaySalary(int id)
    {
        var employee = _objBLEmployee.Get(id);
        if (employee != null)
        {
            _bank.ProcessSalary(employee, employee.P01F08);
            Console.WriteLine($"Salary of {employee.P01F08} paid to {employee.P01F02} through {BankName}.");
        }
    }
}
