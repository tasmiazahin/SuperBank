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
                Console.WriteLine($"loggedIn user id : {foundItem.UserId}");





            bool repeat = true;
                while (repeat)
                {
                    Console.WriteLine("Select your option");
                    Console.WriteLine("[1] See your accounts and balance ");
                    Console.WriteLine("[2] Transfer between accounts");
                    Console.WriteLine("[3] Withdraw money");
                    Console.WriteLine("[4] Deposit money");
                    Console.WriteLine("[5] Open a new account");
                    Console.WriteLine("[6] Log out");
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
                                Double.TryParse(Console.ReadLine(), out Double amount);

                                foundItem.BankAccounts[transferFrom - 1].MakeWithdrawal(amount, DateTime.Now, $"Transfer to another account {foundItem.BankAccounts[transferTo-1].AccountNumber}");
                                foundItem.BankAccounts[transferTo - 1].MakeDeposit(amount, DateTime.Now, $"New deposit from account {foundItem.BankAccounts[transferFrom - 1].AccountNumber}");

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
                                double.TryParse(Console.ReadLine(), out double withdrwalAmount);
                                foundItem.BankAccounts[accountInput - 1].MakeWithdrawal(withdrwalAmount, DateTime.Now, $"Withdrwal money");
                                tryPinCodeAgain = false;
                            }
                            else
                            {
                                Console.WriteLine("You have entered wrong pincode, Try again!");
                            }

                        }
                        
                        break;


                    case 4:

                        foreach (var item in foundItem.BankAccounts)
                        {
                            Console.WriteLine($"Account type : {item.AccountType}, Account Balance {item.Balance}, Account Pincode {item.PinCode}");
                        }
                        Console.WriteLine("Enter your account type where you want to deposit money");

                        for (int i = 0; i < foundItem.BankAccounts.Count; i++)
                        {
                            Console.WriteLine($"Choose {i + 1} for Account type {foundItem.BankAccounts[i].AccountType}");
                        }
                        Int32.TryParse(Console.ReadLine(), out int selectedAccount);

                        bool tryPinAgain = true;
                        while (tryPinAgain)
                        {
                            Console.WriteLine("Enter your Pincode");
                            string pinCodeInput = Console.ReadLine();

                            if (foundItem.BankAccounts[selectedAccount - 1].PinCode == pinCodeInput)
                            {
                                Console.WriteLine("Enter amount");
                                double.TryParse(Console.ReadLine(), out double depositAmount);
                                foundItem.BankAccounts[selectedAccount - 1].MakeDeposit(depositAmount, DateTime.Now, $"Deposit money");
                                tryPinAgain = false;
                            }
                            else
                            {
                                Console.WriteLine("You have entered wrong pincode, Try again!");
                            }
                        }
                         break;

                    case 5:
                        Console.WriteLine("Open a new account");
                        Console.WriteLine("Enter account type, 0 for CurrentAccount, 1 for SavingsAccount, 2 for StudentAccount, 3 for SalaryAccount, 4 for JoinAccount");

                        AccountType type = (AccountType) Convert.ToInt32(Console.ReadLine());

                        Random random = new Random();

                        string pinCode = random.Next(1000, 9999).ToString();

                        foundItem.BankAccounts.Add(new BankAccount(foundItem.UserId, 0, type, pinCode));

                        Console.WriteLine($"found item total bankaccount {foundItem.BankAccounts.Count}");
                        Console.WriteLine($"User1 total bankaccount {Users[0].BankAccounts.Count}");

                        break;


                    case 6:
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








