$(document).ready(function () {
  console.log("main.js file is linked !!!");
  // * ==============================================================================
  // ! Bank class
  // * ==============================================================================
  class Bank {
    constructor() {
      this.accounts = []; // pela default ma empty array rakhel che
    }

    openAccount(accountHolderName, initialBalance = 0) {
      const accountNumber = this.accounts.length + 1;
      const newAccount = new Account(
        accountNumber,
        accountHolderName,
        initialBalance
      );
      this.accounts.push(newAccount); // navu banelu accunt main accounts array ma push karel che
      console.log(
        `Account opened: ${newAccount.accountHolderName}, Balance: ${newAccount.balance}`
      );
      return newAccount;
    }

    findAccount(accountNumber) {
      console.log("Finding account:", accountNumber);
      return (
        this.accounts.find((acc) => acc.accountNumber === accountNumber) || null
      );
    }
  }

  // * ==============================================================================
  // ! Account class
  // * ==============================================================================

  class Account {
    constructor(accountNumber, accountHolderName, initialBalance = 0) {
      this.accountNumber = accountNumber;
      this.accountHolderName = accountHolderName;
      this.balance = initialBalance;
      this.transactions = []; // navu account bane tyare default ma koi transactions na hoi
      console.log(
        `Account created: ${accountHolderName}, Balance: ${this.balance}`
      );
    }

    deposit(amount, type = 0) {
      // handle negative deposit amount by user
      if (amount <= 0) throw new Error("Deposit amount must be positive.");
      // handle positive deposit amount by user
      this.balance += amount;
      if (type == 0) {
        // handle duplicate entry in history table
        this.addTransaction("Deposit", amount); // first value type che transaction ni ne second amount
      }
      return this.balance; // return undated balance to caller function
    }

    withdraw(amount, type = 0) {
      // handle negative withdraw amount by user
      if (amount <= 0) throw new Error("Withdrawal amount must be positive.");
      // handle withdraw amount by user which leads to insufficient funds.
      if (amount > this.balance) throw new Error("Insufficient funds.");
      // handle withdraw amount by user when all good !!!.
      this.balance -= amount;
      if (type == 0) {
        // handle duplicate entry in history table
        this.addTransaction("Withdrawal", amount); // first value type che transaction ni ne second amount
      }
      return this.balance; // return undated balance to caller function
    }
    transfer(destinationAccount, amount) {
      // handle negative transfer amount by user
      if (amount <= 0) throw new Error("Transfer amount must be positive.");
      // handle transfer amount by user which leads to insufficient funds.
      if (this.balance < amount) throw new Error("Insufficient funds.");
      // handle transfer amount by user when all good !!!.
      this.withdraw(amount, 1); // Withdraw from source account
      destinationAccount.deposit(amount, 1); // Deposit to destination account

      this.addTransaction("Transfer Out", amount); // first value type che transaction ni ne second amount
      destinationAccount.addTransaction("Transfer In", amount); // first value type che transaction ni ne second amount

      return {
        sourceAccountBalance: this.balance,
        destinationAccountBalance: destinationAccount.balance,
      };
    }
    addTransaction(type, amount) {
      // type a text label che second amount
      const transaction = {
        type,
        amount,
        date: new Date().toLocaleString(),
        balanceAfterTransaction: this.balance,
      };
      this.transactions.push(transaction); // new row added to table of transaction of a specific account.
      console.log(
        `Transaction added: ${type}, Amount: ${amount}, New Balance: ${this.balance}`
      );
    }

    getTransactionHistory() {
      return this.transactions;
    }
  }

  // * ==============================================================================
  // ! DataStore class to handle localStorage
  // * ==============================================================================

  class DataStore {
    static saveAccounts(accounts) {
      localStorage.setItem("accounts", JSON.stringify(accounts)); // accounts array of object che ene string ma convert kari ne local storage ma store karel che
      console.log("Accounts saved to localStorage:", accounts); // debug accounts list
    }

    static loadAccounts() {
      const data = localStorage.getItem("accounts"); // localstorage ni key => account , value => arrayOfObj of account in string form
      if (data) {
        // localstorage ma data hase to j chalse
        const accountsData = JSON.parse(data); // string to obj conversion
        console.log("Loaded accounts from localStorage:", accountsData);
        return accountsData.map((accData) => {
          const account = new Account(
            accData.accountNumber,
            accData.accountHolderName,
            accData.balance || 0
          );
          account.transactions = accData.transactions || [];
          return account;
        });
      }
      console.log("No accounts found in localStorage.");
      return [];
    }
  }

  // * ==============================================================================
  // ! Initialize Bank and load accounts from localStorage
  // * ==============================================================================

  const bank = new Bank();
  const savedAccounts = DataStore.loadAccounts();

  // * ==============================================================================
  // ! Reload saved accounts into the bank
  // * ==============================================================================

  savedAccounts.forEach((accData) => {
    bank.accounts.push(accData);
  });

  // * ==============================================================================
  // ! Open Account form submission
  // * ==============================================================================

  $("#openAccountForm").on("submit", function (e) {
    e.preventDefault();
    const name = $("#accountHolderName").val().trim();
    const initialBalance = $("#initialBalance").val().trim();

    // Validate account holder name
    const nameRegex = /^[A-Za-z\s]+$/;
    const minNameLength = 3;
    const maxNameLength = 50;

    if (name === "") {
      alert("Please enter the account holder's name.");
      return;
    }

    if (!nameRegex.test(name)) {
      alert("Account holder name must contain only letters and spaces.");
      return;
    }

    if (name.length < minNameLength || name.length > maxNameLength) {
      alert(
        `Account holder name must be between ${minNameLength} and ${maxNameLength} characters.`
      );
      return;
    }

    // Validate initial balance
    if (
      initialBalance === "" ||
      isNaN(initialBalance) ||
      parseFloat(initialBalance) < 0
    ) {
      alert("Please enter a valid initial balance (non-negative number).");
      return;
    }

    try {
      const newAccount = bank.openAccount(name, parseFloat(initialBalance));
      alert(
        `Account created successfully! Account Number: ${newAccount.accountNumber}`
      );
      DataStore.saveAccounts(bank.accounts);
      renderAccountsTable(bank.accounts);

      // Clear input fields
      $("#accountHolderName").val("");
      $("#initialBalance").val("");
      $("#email").val("");
    } catch (err) {
      alert(err.message);
    }
  });

  // * ==============================================================================
  // ! Deposit form submission
  // * ==============================================================================

  $("#depositForm").on("submit", function (e) {
    e.preventDefault();
    const accountNumber = parseInt($("#depositAccountNumber").val());
    const amount = parseFloat($("#depositAmount").val());
    console.log("Deposit form submitted:", accountNumber, amount);

    try {
      const account = bank.findAccount(accountNumber);
      console.log("Found account:", account);

      if (account) {
        account.deposit(amount);
        alert(`Deposited ${amount} to account ${accountNumber}.`);
        DataStore.saveAccounts(bank.accounts);
        renderAccountsTable(bank.accounts);
      } else {
        alert("Account not found.");
      }
      $("#depositAccountNumber").val("");
      $("#depositAmount").val("");
    } catch (err) {
      alert(err.message);
    }
  });

  // * ==============================================================================
  // ! Withdraw form submission
  // * ==============================================================================

  $("#withdrawForm").on("submit", function (e) {
    e.preventDefault();
    const accountNumber = parseInt($("#withdrawAccountNumber").val());
    const amount = parseFloat($("#withdrawAmount").val());
    console.log("Withdraw form submitted:", accountNumber, amount);

    try {
      const account = bank.findAccount(accountNumber);
      console.log("Found account:", account);

      if (account) {
        account.withdraw(amount);
        alert(`Withdrew ${amount} from account ${accountNumber}.`);
        DataStore.saveAccounts(bank.accounts);
        renderAccountsTable(bank.accounts);
      } else {
        alert("Account not found.");
      }
      $("#withdrawAccountNumber").val("");
      $("#withdrawAmount").val("");
    } catch (err) {
      alert(err.message);
    }
  });

  // * ==============================================================================
  // ! Transfer form submission
  // * ==============================================================================

  $("#transferForm").on("submit", function (e) {
    e.preventDefault();

    const sourceAccountNumber = parseInt($("#sourceAccountNumber").val());
    const destinationAccountNumber = parseInt(
      $("#destinationAccountNumber").val()
    );
    if (sourceAccountNumber === destinationAccountNumber) {
      alert("Sender and receiver must be different.");
      return;
    }
    const amount = parseFloat($("#transferAmount").val());

    console.log(
      "Transfer form submitted:",
      sourceAccountNumber,
      destinationAccountNumber,
      amount
    );

    try {
      const sourceAccount = bank.findAccount(sourceAccountNumber);
      const destinationAccount = bank.findAccount(destinationAccountNumber);

      if (!sourceAccount) {
        alert("Source account not found.");
        return;
      }
      if (!destinationAccount) {
        alert("Destination account not found.");
        return;
      }

      const { sourceAccountBalance, destinationAccountBalance } =
        sourceAccount.transfer(destinationAccount, amount); // akhi row return karse emathi destructure karel che 2 values
      alert(
        `Transferred ${amount} from account ${sourceAccountNumber} to account ${destinationAccountNumber}.`
      );

      // Update accounts and render the updated table
      DataStore.saveAccounts(bank.accounts);
      renderAccountsTable(bank.accounts);
      $("#sourceAccountNumber").val("");
      $("#destinationAccountNumber").val("");
      $("#transferAmount").val("");
    } catch (err) {
      alert(err.message);
    }
  });

  // * ==============================================================================
  // ! Render accounts table in the UI
  // * ==============================================================================

  function renderAccountsTable(accounts) {
    // $("#historyModal").modal("hide");
    const tableBody = $("#accountsTableBody");
    tableBody.empty();

    accounts.forEach((acc) => {
      const row = `
        <tr>
          <td>${acc.accountNumber}</td>
          <td>${acc.accountHolderName}</td>
          <td>${acc.balance}</td>
          <td>
            <button class="view-transactions btn btn-primary" data-id="${acc.accountNumber}" >View</button>
          </td>
        </tr>
      `;
      tableBody.append(row);
    });

    // * ==============================================================================
    // ! Handle View Transactions button click [inner]
    // * ==============================================================================

    $(".view-transactions").on("click", function () {
      const accountNumber = $(this).data("id"); // j row nu view button click thayu che e row mathi id find kari
      const account = bank.findAccount(accountNumber);
      if (account) {
        renderTransactionHistory(account.getTransactionHistory());
      }
    });
  }

  // * ==============================================================================
  // ! Render transaction history modal [outer]
  // * ==============================================================================

  function renderTransactionHistory(transactions) {
    const modalBody = $("#transactionHistoryModalBody");
    modalBody.empty(); // juna koi account ni history clear kari

    transactions.forEach((tx) => {
      const row = `
        <tr>
          <td>${tx.type}</td>
          <td>${tx.amount}</td>
          <td>${tx.date}</td>
          <td>${tx.balanceAfterTransaction}</td>
        </tr>
      `;
      modalBody.append(row);
    });

    $("#transactionHistoryModal").modal("show"); // specific account ni history vadu modal trigger karel che
  }

  // * ==============================================================================
  // ! Initial Rendering of accounts table
  // * ==============================================================================

  renderAccountsTable(bank.accounts);

  // * ==============================================================================
  // ! usd to inr conversion modal code
  // * ==============================================================================

  const apiUrl = "http://apilayer.net/api/live"; // apilayer endpoint for live exchange rates
  const apiKey = "d0b165ffbb38c1c39b92cc7c43485a01";

  // When the "Check USD Exchange Rates" button is clicked
  $("#checkUSDButton").on("click", function () {
    // Open the modal
    $("#exchangeRateModal").modal("show");
    getExchangeRates();
  });

  // * ==============================================================================
  // ! Function to get exchange rates from apilayer API
  // * ==============================================================================

  function getExchangeRates() {
    $.ajax({
      url: apiUrl,
      method: "GET",
      data: {
        access_key: apiKey, // API key
        currencies: "INR", // Fetch INR currency rate
        source: "USD", // USD as the base currency
        format: 1, // Format the response as JSON (other : text, script, html ....)
      },
      success: function (response) {
        if (response && response.quotes) {
          //quotes are as per api docs given in github read.me
          const usdINRRate = response.quotes.USDINR; // storing today's usd to inr rate by api

          $("#rateInfo").html(`
              <p><strong>USD to INR:</strong> ₹${usdINRRate}</p>
            `);

          // Store the rate for later conversion
          $("#convertButton").data("rate", usdINRRate); // Storing USD to INR rate jeti calculate kari saki input field mate
        } else {
          $("#rateInfo").text("Error fetching exchange rates.");
        }
      },
      error: function () {
        $("#rateInfo").text("Error fetching exchange rates.");
      },
    });
  }

  // * ==============================================================================
  // ! When the "Convert to USD" button is clicked
  // * ==============================================================================

  $("#convertButton").on("click", function () {
    const inrAmount = parseFloat($("#inrAmount").val());
    const usdToInrRate = $(this).data("rate"); // Get the stored USD to INR rate

    if (isNaN(inrAmount) || inrAmount <= 0) {
      alert("Please enter a valid INR amount.");
    } else {
      const usdAmount = inrAmount / usdToInrRate; // Conversion formula
      $("#convertedAmount").text(
        `Converted Amount: $${usdAmount.toFixed(2)} USD` // point pachi 2j value apva
      );
    }
    $("#inrAmount").val("");
  });
});
