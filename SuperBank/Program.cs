namespace SuperBank;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Bank");

        User user1 = new User("Tasmia", "1234");
        user1.BankAccounts.Add(new BankAccount(user1.UserId, 0, AccountType.SalaryAccount));
        user1.BankAccounts.Add(new BankAccount(user1.UserId, 100, AccountType.SavingsAccount));

        User user2 = new User("Alex", "12345");
        user2.BankAccounts.Add(new BankAccount(user2.UserId, 0, AccountType.SalaryAccount));

        User user3 = new User("Maria", "abcd");
        user3.BankAccounts.Add(new BankAccount(user3.UserId, 100, AccountType.SalaryAccount));
        user3.BankAccounts.Add(new BankAccount(user3.UserId, 500, AccountType.SavingsAccount));

        User user4 = new User("Patrik", "xyz");
        user4.BankAccounts.Add(new BankAccount(user4.UserId, 1000, AccountType.SalaryAccount));
        user4.BankAccounts.Add(new BankAccount(user4.UserId, 2000, AccountType.SavingsAccount));


        User user5 = new User("Elsa", "qwer");
        user5.BankAccounts.Add(new BankAccount(user5.UserId, 500, AccountType.SalaryAccount));
        user5.BankAccounts.Add(new BankAccount(user5.UserId, 1000, AccountType.SavingsAccount));
        

        User[] Users = new User[] {
            user1, user2, user3, user4, user5
        };

        foreach (var item in Users)
        {
            Console.WriteLine("User Id: "+item.UserId + " User Name: " + item.UserName + " Number of Bank account: " + item.BankAccounts.Count());
            foreach (var account in item.BankAccounts)
            {
                Console.WriteLine("Account number : " +account.AccountNumber +" Acccount Type: " + account.AccountType + " Balance: " + account.Balance);
            }
            Console.WriteLine("-----------------");
        }

        newLogin:
            Console.WriteLine("Welcome to your account, Please login");
            Console.WriteLine("Enter your username");
            string username = Console.ReadLine();

            Console.WriteLine("Enter your password");
            string password = Console.ReadLine();


            var foundItem = Array.Find(Users, item => item.UserName == username && item.Password == password);

            if (foundItem != null)
            {
           
                Console.WriteLine("You have logged in successfully!");
                bool repeat = true;
                while (repeat)
                {
                    Console.WriteLine("Select your option");
                    Console.WriteLine("[1] See your accounts and balance ");
                    Console.WriteLine("[2] Transfer between accounts");
                    Console.WriteLine("[3] Withdraw money");
                    Console.WriteLine("[4] Log out");
                    Int32.TryParse(Console.ReadLine(), out int input);


                    switch (input)
                    {
                        case 1:
                            Console.WriteLine("Your account balance is xx");
                            break;

                        case 2:
                            Console.WriteLine("Your have transfered 100 from account a to account b");
                            break;

                        case 3:
                            Console.WriteLine("You have withdraw money ");
                            break;

                        case 4:
                            Console.WriteLine("You have logged out ");
                            repeat = false;
                            goto newLogin;
                            break;

                        default:
                            Console.WriteLine("You have to choose option between 1-4");
                            break;
                    }

                    Console.WriteLine("Please press Enter to go to the main menu");

                    while (Console.ReadKey().Key != ConsoleKey.Enter)
                    {
                        Console.WriteLine("\nPress enter to go to main menu");
                    }
                }
            }
            else
            {
                Console.WriteLine("Wrong username or password, Please try again");
                goto newLogin;
            }

            Console.ReadLine();

    }
}








