using System;
namespace SuperBank
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<BankAccount> BankAccounts = new List<BankAccount>();
        private static int Id = 1;

        public User(string username, string password)
        {
            this.UserName = username;
            this.Password = password;
            this.UserId = Id;
            Id++;
        }
    }
}
