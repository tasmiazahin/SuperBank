namespace SuperBank;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Bank");

        User user1 = new User("Tasmia", "1234");
        user1.BankAccounts.Add(new BankAccount("aaa", 0));
        user1.BankAccounts.Add(new BankAccount("bbb", 100));

        User user2 = new User("Alex", "1234");
        user2.BankAccounts.Add(new BankAccount("xyz", 0));

        User user3 = new User("Maria", "12334");
        user3.BankAccounts.Add(new BankAccount("kkk", 220));
        user3.BankAccounts.Add(new BankAccount("ccc", 700));

        User user4 = new User("Patrik", "1233004");
        user4.BankAccounts.Add(new BankAccount("kkooik", 2200));
        user4.BankAccounts.Add(new BankAccount("cccuyg", 900));


        User user5 = new User("Elsa", "12338004");
        user5.BankAccounts.Add(new BankAccount("kkoffoik", 1200));
        user5.BankAccounts.Add(new BankAccount("cyuyg", 980));


        User[] Users = new User[] {
            user1, user2, user3, user4, user5
        };

        foreach (var item in Users)
        {
            Console.WriteLine(item.UserName + "  "+item.BankAccounts.Count());
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








