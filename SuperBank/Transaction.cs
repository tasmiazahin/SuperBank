using System;
namespace SuperBank
{
    public class Transaction
    {
        public double Amount { get; }
        public DateTime Date { get; }
        public string Notes { get; }

        public Transaction(double amount, DateTime date, string note)
        {
            Amount = amount;
            Date = date;
            Notes = note;
        }
    }
}

