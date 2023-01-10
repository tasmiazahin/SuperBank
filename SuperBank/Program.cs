using System.Collections.Generic;

namespace SuperBank;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Bank");

        User user1 = new User("Tasmia", "1234");
        user1.BankAccounts.Add(new BankAccount(user1.UserId, 10000, AccountType.SalaryAccount , "3459", CurrencyType.Euro));
        user1.BankAccounts.Add(new BankAccount(user1.UserId, 15000, AccountType.SavingsAccount , "2341" ,CurrencyType.SEK));

        User user2 = new User("Alex", "12345");
        user2.BankAccounts.Add(new BankAccount(user2.UserId, 5000, AccountType.SalaryAccount, "7845", CurrencyType.SEK));

        User user3 = new User("Maria", "abcd");
        user3.BankAccounts.Add(new BankAccount(user3.UserId, 4000, AccountType.SalaryAccount ,"4327" , CurrencyType.SEK));
        user3.BankAccounts.Add(new BankAccount(user3.UserId, 20000, AccountType.SavingsAccount, "9724" , CurrencyType.Euro));

        User user4 = new User("Patrik", "xyz");
        user4.BankAccounts.Add(new BankAccount(user4.UserId, 1000, AccountType.SalaryAccount,"4567", CurrencyType.SEK));
        user4.BankAccounts.Add(new BankAccount(user4.UserId, 2000, AccountType.SavingsAccount,"1690", CurrencyType.SEK));


        User user5 = new User("Elsa", "qwer");
        user5.BankAccounts.Add(new BankAccount(user5.UserId, 500, AccountType.SalaryAccount, "8856", CurrencyType.SEK));
        user5.BankAccounts.Add(new BankAccount(user5.UserId, 1000, AccountType.SavingsAccount,"3486", CurrencyType.Euro));
        user5.BankAccounts.Add(new BankAccount(user5.UserId, 5000, AccountType.JoinAccount, "9876", CurrencyType.SEK));



        User[] Users = new User[] {
            user1, user2, user3, user4, user5
        };

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
                    Console.WriteLine("[4] Deposit money");
                    Console.WriteLine("[5] Open a new account");
                    Console.WriteLine("[6] Log out");
                    Int32.TryParse(Console.ReadLine(), out int input);


                    switch (input)
                    {
                        case 1:
                            foreach (var item in foundItem.BankAccounts)
                            {
                                Console.WriteLine($"Account number {item.AccountNumber} Account type {item.AccountType} Account balance {item.Balance} {item.CurrencyType}");
                            }
                            break;

                        case 2:

                            if (foundItem.BankAccounts.Count()>1)
                            {
                                Console.WriteLine("Enter your account number from where you want to transfer");

                                for (int i = 0; i < foundItem.BankAccounts.Count; i++)
                                {
                                    Console.WriteLine($"Enter {i+1} for AccountNumber {foundItem.BankAccounts[i].AccountNumber}");
                                }
                                Int32.TryParse(Console.ReadLine(), out int transferFrom);

                                


                                Console.WriteLine("Enter your account number where you want to transfer");

                                for (int i = 0; i < foundItem.BankAccounts.Count; i++)
                                {
                                    Console.WriteLine($"Enter {i + 1} for AccountNumber {foundItem.BankAccounts[i].AccountNumber}");
                                }
                                Int32.TryParse(Console.ReadLine(), out int transferTo);




                                

                                Console.WriteLine($"Enter amount to transfer, current balance is {foundItem.BankAccounts[transferFrom - 1].Balance} {foundItem.BankAccounts[transferFrom - 1].CurrencyType}");
                                Double.TryParse(Console.ReadLine(), out Double amount);

                                Double amountToDeposit = 0;

                                // Conversion
                                // 1 Euro = 10 SEK
                                // 1 SEK = 0.1 Euro
                                if (foundItem.BankAccounts[transferFrom - 1].CurrencyType == CurrencyType.Euro && foundItem.BankAccounts[transferTo - 1].CurrencyType == CurrencyType.SEK)
                                {
                                    amountToDeposit = amount * 10;

                                }
                                else if (foundItem.BankAccounts[transferFrom - 1].CurrencyType == CurrencyType.SEK && foundItem.BankAccounts[transferTo - 1].CurrencyType == CurrencyType.Euro)
                                {
                                    amountToDeposit = amount / 10;
                                }

                                foundItem.BankAccounts[transferFrom - 1].MakeWithdrawal(amount, DateTime.Now, $"Transfer to another account {foundItem.BankAccounts[transferTo-1].AccountNumber}");
                                foundItem.BankAccounts[transferTo - 1].MakeDeposit(amountToDeposit, DateTime.Now, $"New deposit from account {foundItem.BankAccounts[transferFrom - 1].AccountNumber}");

                            }
                            else
                            {
                                Console.WriteLine("You must have more than one account");
                            }
                          break;

                    case 3:
                        Console.WriteLine("Select your account from you want to withdraw");

                        for (int i = 0; i < foundItem.BankAccounts.Count; i++)
                        {
                            Console.WriteLine($"Enter {i + 1} for Account number {foundItem.BankAccounts[i].AccountNumber}");
                        }
                        Int32.TryParse(Console.ReadLine(), out int accountInput);


                        bool tryPinCodeAgain = true;
                        int attemptLeft = 3;
                        while (tryPinCodeAgain)
                        {
                            Console.WriteLine($"Total attempt left {attemptLeft}");
                            Console.WriteLine("Enter your account Pincode");
                            string pinCodeInput = Console.ReadLine();
                            attemptLeft--;

                            if (foundItem.BankAccounts[accountInput - 1].PinCode == pinCodeInput)
                            {
                                Console.WriteLine("Enter amount to withdraw");
                                double.TryParse(Console.ReadLine(), out double withdrwalAmount);
                                foundItem.BankAccounts[accountInput - 1].MakeWithdrawal(withdrwalAmount, DateTime.Now, $"Withdrwal money");
                                tryPinCodeAgain = false;
                            }
                            else
                            {
                               Console.WriteLine("You have entered wrong pincode, Please try again!");
                                if (attemptLeft == 0)
                                {
                                    Console.WriteLine($"Too many wrong  attempt! You would need wait 3 minutes to try again, start delay at {DateTime.Now}");
                                    // Will delay for three seconds
                                    Thread.Sleep(3*60*1000);
                                    Console.WriteLine($"Finished delay at {DateTime.Now}");
                                    attemptLeft = 3;
                                }
                            }
                        }
                        
                        break;

                    case 4:
                         DepositToAccount(foundItem.BankAccounts);
                         break;

                    case 5:
                        CreateBankAccount(foundItem.UserId, foundItem.BankAccounts);
                        break;

                    case 6:
                        Console.WriteLine("You have logged out! ");
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

    public static void DepositToAccount(List<BankAccount> accounts)
    {
        Console.WriteLine("Select your account where you want to deposit");

        for (int i = 0; i < accounts.Count; i++)
        {
            Console.WriteLine($"Enter {i + 1} for Account number {accounts[i].AccountNumber}");
        }
        Int32.TryParse(Console.ReadLine(), out int selectedAccount);

        bool tryPinAgain = true;
        int attemptLeft = 3;
        while (tryPinAgain)
        {
            Console.WriteLine($"Total attempt left {attemptLeft}");
            Console.WriteLine("Enter your Pincode");
            string pinCodeInput = Console.ReadLine();
            attemptLeft--;

            if (accounts[selectedAccount - 1].PinCode == pinCodeInput)
            {
                Console.WriteLine("Enter amount to deposit");
                double.TryParse(Console.ReadLine(), out double depositAmount);
                accounts[selectedAccount - 1].MakeDeposit(depositAmount, DateTime.Now, $"Deposit money");
                tryPinAgain = false;
            }
            else
            {
                Console.WriteLine("You have entered wrong pincode, Please try again!");
                if (attemptLeft == 0)
                {
                    Console.WriteLine($"Too many wrong  attempts! You would need wait 3 minutes to try again, start delay at {DateTime.Now}");
                    // Will delay for three seconds
                    Thread.Sleep(3 * 60 * 1000);
                    Console.WriteLine($"Finished delay at {DateTime.Now}");
                    attemptLeft = 3;
                }
            }
        }
    }

    public static void CreateBankAccount(int userId, List<BankAccount> accounts)
    {

        Console.WriteLine("Open a new account");
        Console.WriteLine("Enter account type, 0 for CurrentAccount, 1 for SavingsAccount, 2 for StudentAccount, 3 for SalaryAccount, 4 for JoinAccount");
 

        AccountType type = (AccountType)Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter your currency type, 0 for Euro, 1 for SEK");
        CurrencyType currencyType = (CurrencyType)Convert.ToInt32(Console.ReadLine());

        Random random = new Random();
        string pinCode = random.Next(1000, 9999).ToString();

        accounts.Add(new BankAccount(userId, 0, type, pinCode, currencyType));

    }
}








