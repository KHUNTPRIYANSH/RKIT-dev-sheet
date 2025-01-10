using System.Data;
using LastLib;
namespace CSharpFinalDemo
{
    #region Models
    /// <summary>
    /// Represents the account information.
    /// </summary>
    public class AccountInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AadhaarNumber { get; set; }
        public string PANNumber { get; set; }
        public string AccountType { get; set; }
        public int Balance { get; set; } = 1000;

        public AccountInfo(int id, string name, string aadhaar, string pan, string accountType)
        {
            Id = id;
            Name = name;
            AadhaarNumber = aadhaar;
            PANNumber = pan;
            AccountType = accountType;
        }
    }

    /// <summary>
    /// Represents a transaction between accounts.
    /// </summary>
    public class Transaction
    {
        public int FromId { get; set; }
        public int ToId { get; set; }
        public int Amount { get; set; }

        public Transaction(int fromId, int toId, int amount)
        {
            FromId = fromId;
            ToId = toId;
            Amount = amount;
        }
    }


    #endregion

    #region Bank Operations
    /// <summary>
    /// Represents the bank and its operations.
    /// </summary>
    public class Bank
    {
        private List<AccountInfo> accounts = new List<AccountInfo>();
        private List<Transaction> transactions = new List<Transaction>();
        private DataTable accountTable = new DataTable();

        public Bank()
        {
            accountTable.Columns.Add("Id", typeof(int));
            accountTable.Columns.Add("Name", typeof(string));
            accountTable.Columns.Add("AadhaarNumber", typeof(string));
            accountTable.Columns.Add("PANNumber", typeof(string));
            accountTable.Columns.Add("AccountType", typeof(string));
            accountTable.Columns.Add("Balance", typeof(int));
        }

        #region Account Management
        /// <summary>
        /// Creates a new account.
        /// </summary>
        public void CreateAccount()
        {
            try
            {
                Console.Clear();
                Console.Write("Enter your name: ");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentException("Name cannot be null or empty.");

                Console.Write("Enter your Aadhaar number: ");
                string aadhaar = Console.ReadLine();
                long parsedValue;
                if (aadhaar.Length != 12 || !long.TryParse(aadhaar, out parsedValue))
                    throw new ArgumentException("Invalid Aadhaar number. It must be 12 digits.");

                Console.Write("Enter your PAN number: ");
                string pan = Console.ReadLine();

                Console.Write("Enter account type (Savings, Business): ");
                string accountType = Console.ReadLine();

                int id = accounts.Count;
                var account = new AccountInfo(id, name, aadhaar, pan, accountType);
                accounts.Add(account);

                DataRow row = accountTable.NewRow();
                row["Id"] = account.Id;
                row["Name"] = account.Name;
                row["AadhaarNumber"] = account.AadhaarNumber;
                row["PANNumber"] = account.PANNumber;
                row["AccountType"] = account.AccountType;
                row["Balance"] = account.Balance;
                accountTable.Rows.Add(row);

                Console.WriteLine($"Account created successfully! Your account ID is {id}");
                SaveToFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Deposits money into an account.
        /// </summary>
        public void DepositMoney()
        {
            try
            {
                Console.Clear();
                Console.Write("Enter your account ID: ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("Enter amount to deposit: ");
                int amount = int.Parse(Console.ReadLine());
                if (amount <= 0)
                    throw new ArgumentException("Amount must be positive.");

                var account = accounts.Find(a => a.Id == id);
                if (account == null)
                    throw new ArgumentException("Account not found.");

                account.Balance += amount;
                transactions.Add(new Transaction(id, id, amount));

                Console.WriteLine("Money deposited successfully!");
                SaveToFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Transfers money between accounts.
        /// </summary>
        public void TransferMoney()
        {
            try
            {
                Console.Clear();
                Console.Write("Enter your account ID: ");
                int fromId = int.Parse(Console.ReadLine());

                Console.Write("Enter receiver account ID: ");
                int toId = int.Parse(Console.ReadLine());

                Console.Write("Enter amount to transfer: ");
                int amount = int.Parse(Console.ReadLine());
                if (amount <= 0)
                    throw new ArgumentException("Amount must be positive.");

                var fromAccount = accounts.Find(a => a.Id == fromId);
                var toAccount = accounts.Find(a => a.Id == toId);
                if (fromAccount == null || toAccount == null)
                    throw new ArgumentException("Invalid account ID.");

                if (fromAccount.Balance - amount < 1000)
                    throw new InvalidOperationException("Insufficient balance. Maintain at least 1000.");

                fromAccount.Balance -= amount;
                toAccount.Balance += amount;
                transactions.Add(new Transaction(fromId, toId, amount));

                Console.WriteLine("Money transferred successfully!");
                SaveToFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Withdraws money from an account.
        /// </summary>
        public void WithdrawMoney()
        {
            try
            {
                Console.Clear();
                Console.Write("Enter your account ID: ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("Enter amount to withdraw: ");
                int amount = int.Parse(Console.ReadLine());
                if (amount <= 0)
                    throw new ArgumentException("Amount must be positive.");

                var account = accounts.Find(a => a.Id == id);
                if (account == null)
                    throw new ArgumentException("Account not found.");

                if (account.Balance - amount < 1000)
                    throw new InvalidOperationException("Insufficient balance. Maintain at least 1000.");

                account.Balance -= amount;
                transactions.Add(new Transaction(id, id, -amount));

                Console.WriteLine("Money withdrawn successfully!");
                SaveToFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Displays all accounts.
        /// </summary>
        public void DisplayAccounts()
        {
            Console.Clear();
            if (accounts.Count == 0)
            {
                Console.WriteLine("!!! No accounts in the bank.");
            }
            else
            {
                Console.WriteLine("### Account Information ###\n");
                foreach (var account in accounts)
                {
                    Console.WriteLine($"ID: {account.Id},\tName: {account.Name},\tAadhaar: {account.AadhaarNumber},\tPAN: {account.PANNumber},\tType: {account.AccountType},\tBalance: {account.Balance}");
                }
            }
        }
        #endregion

        #region File Operations
        private void SaveToFile()
        {
            using (StreamWriter writer = new StreamWriter("accounts.txt"))
            {
                foreach (var account in accounts)
                {
                    writer.WriteLine($"{account.Id},{account.Name},{account.AadhaarNumber},{account.PANNumber},{account.AccountType},{account.Balance}");
                }
            }
        }

        public void LoadFromFile()
        {
            if (!File.Exists("accounts.txt")) return;

            using (StreamReader reader = new StreamReader("accounts.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');
                    var account = new AccountInfo(int.Parse(data[0]), data[1], data[2], data[3], data[4])
                    {
                        Balance = int.Parse(data[5])
                    };
                    accounts.Add(account);
                }
            }
        }
        #endregion

        #region Main Menu
        /// <summary>
        /// Displays the main menu for the bank operations.
        /// </summary>
        public void MainMenu()
        {
            CurrencyConverter converter = new CurrencyConverter();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Create Account\n2. Deposit Money\n3. Withdraw Money\n4. Transfer Money\n5. Display Accounts\n6. INR to USD Conversion\n7. Exit");
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        CreateAccount();
                        break;
                    case 2:
                        DepositMoney();
                        break;
                    case 3:
                        WithdrawMoney();
                        break;
                    case 4:
                        TransferMoney();
                        break;
                    case 5:
                        DisplayAccounts();
                        break;
                    case 6:
                        Console.Write("Enter the amount in INR: ");
                        float amountInRupees = float.Parse(Console.ReadLine());
                        converter.ConvertAndPrintAmount(amountInRupees);
                        break;
                    case 7:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
        #endregion
    }
    #endregion

    #region Main Class
    class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();
            bank.LoadFromFile();
            bank.MainMenu();
        }
    }
    #endregion
}
