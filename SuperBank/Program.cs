namespace SuperBank;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Bank");

        User user1 = new User("Tasmia", "1234");
        user1.BankAccounts.Add(new BankAccount(user1.UserId, 10000, AccountType.SalaryAccount , "3459"));
        user1.BankAccounts.Add(new BankAccount(user1.UserId, 15000, AccountType.SavingsAccount , "2341"));

        User user2 = new User("Alex", "12345");
        user2.BankAccounts.Add(new BankAccount(user2.UserId, 5000, AccountType.SalaryAccount, "7845"));

        User user3 = new User("Maria", "abcd");
        user3.BankAccounts.Add(new BankAccount(user3.UserId, 4000, AccountType.SalaryAccount ,"4327"));
        user3.BankAccounts.Add(new BankAccount(user3.UserId, 20000, AccountType.SavingsAccount, "9724"));

        User user4 = new User("Patrik", "xyz");
        user4.BankAccounts.Add(new BankAccount(user4.UserId, 1000, AccountType.SalaryAccount,"4567"));
        user4.BankAccounts.Add(new BankAccount(user4.UserId, 2000, AccountType.SavingsAccount,"1690"));


        User user5 = new User("Elsa", "qwer");
        user5.BankAccounts.Add(new BankAccount(user5.UserId, 500, AccountType.SalaryAccount, "8856"));
        user5.BankAccounts.Add(new BankAccount(user5.UserId, 1000, AccountType.SavingsAccount,"3486"));
        user5.BankAccounts.Add(new BankAccount(user5.UserId, 5000, AccountType.JoinAccount, "9876"));


        User[] Users = new User[] {
            user1, user2, user3, user4, user5
        };

        //foreach (var item in Users)
        //{
        //    Console.WriteLine("User Id: " + item.UserId + " User Name: " + item.UserName + " Number of Bank account: " + item.BankAccounts.Count());
        //    foreach (var account in item.BankAccounts)
        //    {
        //        Console.WriteLine("Account number : " + account.AccountNumber + " Acccount Type: " + account.AccountType + " Balance: " + account.Balance);
        //    }
        //    Console.WriteLine("-----------------");
        //}

    newLogin:
            Console.WriteLine("Welcome to your account, Please login");
            Console.WriteLine("Enter your username");
            string username = Console.ReadLine();

            Console.WriteLine("Enter your password");
            string password = Console.ReadLine();


            var foundItem = Array.Find(Users, item => item.UserName.ToLower() == username.ToLower() && item.Password == password);

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
                            Console.WriteLine($"Yor are logged in as  {foundItem.UserName} and you have {foundItem.BankAccounts.Count()} accounts");
                            foreach (var item in foundItem.BankAccounts)
                            {
                                Console.WriteLine($"Account number {item.AccountNumber} Account type {item.AccountType} Account balance {item.Balance}");
                            }
                            break;

                        case 2:

                            if (foundItem.BankAccounts.Count()>1)
                            {
                                foreach (var item in foundItem.BankAccounts)
                                {
                                    Console.WriteLine($"Account type : {item.AccountType} , Account number : {item.AccountNumber} , Account Balance {item.Balance}");
                                }
                                Console.WriteLine("Enter your accountnumber from where you want to transfer");

                                for (int i = 0; i < foundItem.BankAccounts.Count; i++)
                                {
                                    Console.WriteLine($"Choose {i+1} for AccountNumber {foundItem.BankAccounts[i].AccountNumber}");
                                }
                                Int32.TryParse(Console.ReadLine(), out int transferFrom);
                            



                                Console.WriteLine("Enter your accountnumber where you want to transfer");

                                for (int i = 0; i < foundItem.BankAccounts.Count; i++)
                                {
                                    Console.WriteLine($"Choose {i + 1} for AccountNumber {foundItem.BankAccounts[i].AccountNumber}");
                                }
                                Int32.TryParse(Console.ReadLine(), out int transferTo);


                                Console.WriteLine("Enter amount");
                                Int32.TryParse(Console.ReadLine(), out int withdrwalAmount);

                                foundItem.BankAccounts[transferFrom - 1].MakeWithdrawal(withdrwalAmount, DateTime.Now, $"Transfer to another account {foundItem.BankAccounts[transferTo-1].AccountNumber}");
                                foundItem.BankAccounts[transferTo - 1].MakeDeposit(withdrwalAmount, DateTime.Now, $"New deposit from account {foundItem.BankAccounts[transferFrom - 1].AccountNumber}");

                            }
                            else
                            {
                                Console.WriteLine("You must have more than one account");
                            }
                          break;


                    case 3:
                        
                        foreach (var item in foundItem.BankAccounts)
                        {
                            Console.WriteLine($"Account type : {item.AccountType}, Account Balance {item.Balance}, Account Pincode {item.PinCode}");
                        }
                        Console.WriteLine("Enter your account type from you want to withdraw money");

                        for (int i = 0; i < foundItem.BankAccounts.Count; i++)
                        {
                            Console.WriteLine($"Choose {i + 1} for Account type {foundItem.BankAccounts[i].AccountType}");
                        }
                        Int32.TryParse(Console.ReadLine(), out int accountInput);


                        bool tryPinCodeAgain = true;
                        while (tryPinCodeAgain)
                        {
                            Console.WriteLine("Enter your Pincode");
                            string pinCodeInput = Console.ReadLine();

                            if (foundItem.BankAccounts[accountInput - 1].PinCode == pinCodeInput)
                            {
                                Console.WriteLine("Enter amount");
                                Int32.TryParse(Console.ReadLine(), out int withdrwalAmount);
                                foundItem.BankAccounts[accountInput - 1].MakeWithdrawal(withdrwalAmount, DateTime.Now, $"Withdrawal money");
                                tryPinCodeAgain = false;
                            }
                            else
                            {
                                Console.WriteLine("You have entered wrong pincode, Try again!");
                            }

                        }
                        
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








