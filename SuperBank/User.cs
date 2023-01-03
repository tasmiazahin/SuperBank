using System;
namespace SuperBank
{
    public class User
    {
        public int UserId;
        public string UserFullName;
        public string UserName;
        public string Password;
        public List<BankAccount> BankAccounts = new List<BankAccount>();

        public User(string username, string password)
        {
            UserName = username;
            Password = password;
        }

        public User(string username, string password, List<BankAccount> bankAccounts)
        {
            UserName = username;
            Password = password;
            BankAccounts = bankAccounts;
        }
    }
}
