using System;

namespace LearningCSharp
{
    #region BankAccount Class

    // Encapsulation in action using a BankAccount class
    public class BankAccount
    {
        #region Private Fields

        // Private field to store the account balance (encapsulation)
        private decimal _BalanceVariable;

        #endregion

        #region Public Properties

        // Public property to get the balance (read-only)
        public decimal Balance
        {
            get { return _BalanceVariable; }
        }

        #endregion

        #region Constructor

        // Constructor to initialize the account balance
        public BankAccount(decimal initialBalance)
        {
            if (initialBalance >= 0)
            {
                _BalanceVariable = initialBalance;
            }
            else
            {
                Console.WriteLine("Initial balance cannot be negative.");
                _BalanceVariable = 0;
            }
        }

        #endregion

        #region Public Methods

        // Public method to deposit money into the account
        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                _BalanceVariable += amount;
                Console.WriteLine($"Deposited: {amount:C}. New balance: {_BalanceVariable:C}");
            }
            else
            {
                Console.WriteLine("Deposit amount must be positive.");
            }
        }

        // Public method to withdraw money from the account
        public void Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= _BalanceVariable)
            {
                _BalanceVariable -= amount;
                Console.WriteLine($"Withdrew: {amount:C}. New balance: {_BalanceVariable:C}");
            }
            else
            {
                Console.WriteLine("Invalid withdrawal amount.");
            }
        }

        #endregion
    }

    #endregion

    #region EncapsulationDemo Class

    internal class EncapsulationDemo
    {
        public static void RunEncapsulationDemo()
        {
            #region Demonstration of Encapsulation

            // Create a BankAccount object with an initial balance
            BankAccount myAccount = new BankAccount(1000);

            // Display the initial balance (using the Balance property)
            Console.WriteLine($"Initial balance: {myAccount.Balance:C}");

            // Deposit money into the account
            myAccount.Deposit(500);  // Output: Deposited: 500.00. New balance: 1500.00

            // Withdraw money from the account
            myAccount.Withdraw(200); // Output: Withdrew: 200.00. New balance: 1300.00

            // Try to withdraw more money than the current balance
            myAccount.Withdraw(2000); // Output: Invalid withdrawal amount.

            #endregion
        }
    }

    #endregion
}
