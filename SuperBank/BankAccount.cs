using System;
using System.Transactions;

namespace SuperBank
{
    public enum AccountType
    {
        CurrentAccount,
        SavingsAccount,
        StudentAccount,
        SalaryAccount,
        JoinAccount
    }

    public enum CurrencyType
    {
        Euro,
        SEK,
    }

    public class BankAccount
    {
        public string AccountNumber { get; }
        public AccountType AccountType { get; set; }
        public int OwnerId { get; set; }
        public string PinCode { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public double Balance
        {
            get
            {
                // calculate balance from AllTransactions
                double balance = 0;
                foreach (var item in AllTransactions)
                {
                    balance += item.Amount;
                }

                return balance;
            }
        }

        private readonly List<Transaction> AllTransactions = new List<Transaction>();

        // initial bank account number with 10 digit
        private static int AccountNumberSeed = 1234567890;



        public BankAccount(int ownerId, double initialBalance, AccountType type,string pinCode, CurrencyType currencyType)
        {
            AccountNumber = AccountNumberSeed.ToString();
            AccountNumberSeed++;

            OwnerId = ownerId;
            AccountType = type;
            CurrencyType = currencyType;
            if (initialBalance > 0)
                MakeDeposit(initialBalance, DateTime.Now, "Initial balance");

            PinCode = pinCode;

        }

        public void MakeDeposit(double amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
            }
            // add a transection
            var deposit = new Transaction(amount, date, note);
            AllTransactions.Add(deposit);
        }

        public void MakeWithdrawal(double amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
            }
            if (Balance - amount < 0)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            }
            // add a transection
            var withdrawal = new Transaction(-amount, date, note);
            AllTransactions.Add(withdrawal);
        }

        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();

            double balance = 0;
            report.AppendLine("Date\t\tAmount\tBalance\tNote");
            foreach (var item in AllTransactions)
            {
                balance += item.Amount;
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
            }

            return report.ToString();
        }
    }
}

