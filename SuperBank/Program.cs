using System.Collections.Generic;

namespace SuperBank;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Super Bank");
        //Initiate individual user object
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


        // Create Users array
        User[] Users = new User[] {
            user1, user2, user3, user4, user5
        };

        // label to use goto to transfer control of  the program 
        newLogin:

        // Login process  started
        Console.WriteLine("Welcome to your account, Please login");
        Console.WriteLine("Enter your username");
        string username = Console.ReadLine();

        Console.WriteLine("Enter your password");
        string password = Console.ReadLine();

        //match username  and password and return user object if exists in the user list
        var foundItem = Array.Find(Users, item => item.UserName.ToLower() == username.ToLower() && item.Password == password);

        if (foundItem != null)
        {
            Console.WriteLine("You have logged in successfully!");

            bool repeat = true;
            bool terminateProgram = false;
            // Using while loop to allow logged in user to continuouly return to the main menu.
            while (repeat)
            {
                // menu for logged in user
                Console.WriteLine("Select your option");
                Console.WriteLine("[1] See your accounts and balance ");
                Console.WriteLine("[2] Transfer amount between own accounts");
                Console.WriteLine("[3] Withdraw money");
                Console.WriteLine("[4] Deposit money");
                Console.WriteLine("[5] Open a new account");
                Console.WriteLine("[6] Transfer amount between different user account");
                Console.WriteLine("[7] Log out");
                Console.WriteLine("[8] Terminate the program");
                Int32.TryParse(Console.ReadLine(), out int input);

                
                switch (input)
                {
                    case 1:
                        // Show accounts and balance for logged in user.
                        foreach (var item in foundItem.BankAccounts)
                        {
                            Console.WriteLine($"Account number {item.AccountNumber} Account type {item.AccountType} Account balance {item.Balance} {item.CurrencyType}");
                        }
                        break;

                    case 2:
                        // Number of accounts should be more than 1 to allow loggin in user to transfer amount between his accounts
                        if (foundItem.BankAccounts.Count() > 1)
                        {
                            // Exception handling to prevent program crashing 
                            try
                            {
                                Console.WriteLine("Enter your account number from where you want to transfer");

                                for (int i = 0; i < foundItem.BankAccounts.Count; i++)
                                {
                                    Console.WriteLine($"Enter {i + 1} for AccountNumber {foundItem.BankAccounts[i].AccountNumber}");
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

                                // Currency Conversion and assuming following conversation rate
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
                                else
                                {
                                    amountToDeposit = amount;
                                }

                                // Actual transfer by calling accounts withdrawal and deposite mehtods based on index
                                foundItem.BankAccounts[transferFrom - 1].MakeWithdrawal(amount, DateTime.Now, $"Transfer to another account {foundItem.BankAccounts[transferTo - 1].AccountNumber}");
                                foundItem.BankAccounts[transferTo - 1].MakeDeposit(amountToDeposit, DateTime.Now, $"New deposit from account {foundItem.BankAccounts[transferFrom - 1].AccountNumber}");  
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Transfer failed! \n{ex}");
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("You must have more than one account");
                        }
                        break;

                    case 3:
                        // Exception handling to prevent program crashing 
                        try
                        {
                            Console.WriteLine("Select your account from you want to withdraw");

                            for (int i = 0; i < foundItem.BankAccounts.Count; i++)
                            {
                                Console.WriteLine($"Enter {i + 1} for Account number {foundItem.BankAccounts[i].AccountNumber}");
                            }
                            Int32.TryParse(Console.ReadLine(), out int accountInput);


                            bool tryPinCodeAgain = true;
                            // Introduce number of attemp that will use to pause to try again afte 3 consecutive failurs for 3 minutes
                            int attemptLeft = 3;
                            // Using while loop to try again if wrong picode is given.
                            while (tryPinCodeAgain)
                            {
                                Console.WriteLine($"Total attempt left {attemptLeft}");
                                Console.WriteLine("Enter your account Pincode");
                                string pinCodeInput = Console.ReadLine();
                                attemptLeft--;

                                // match pincode
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
                                        // Will delay thread for three seconds
                                        Thread.Sleep(3 * 60 * 1000);
                                        Console.WriteLine($"Finished delay at {DateTime.Now}");
                                        attemptLeft = 3;
                                    }
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Transfer failed! \n{ex}");
                        }
                        break;

                    case 4:
                        try
                        {
                            DepositToAccount(foundItem.BankAccounts);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Transfer failed! \n{ex}");
                        }
                        break;

                    case 5:
                        try
                        {
                            CreateBankAccount(foundItem.UserId, foundItem.BankAccounts);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Transfer failed! \n{ex}");
                        }
                        break;
                    case 6:
                        try
                        {
                            TransferToOtherUserAccount(Users, foundItem);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Transfer failed! \n{ex}");
                        }
                        break;

                    case 7:
                        Console.WriteLine("You have logged out! ");
                        repeat = false;
                        goto newLogin;
                        break;

                    case 8:
                        Console.WriteLine("Terminated the program! ");
                        repeat = false;
                        terminateProgram = true;
                        break;

                    default:
                        Console.WriteLine("You have to choose option between 1-4");
                        break;
                }
                if (!terminateProgram)
                {
                    Console.WriteLine("Please press Enter to go to the main menu");
                    // Program will wait until user press 'Enter' key 
                    while (Console.ReadKey().Key != ConsoleKey.Enter)
                    {
                        Console.WriteLine("\nPress enter to go to main menu");
                    }

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
        // Introduce number of attemp that will use to pause to try again afte 3 consecutive failurs for 3 minutes
        int attemptLeft = 3;
        // Using while loop to try again if wrong picode is given.
        while (tryPinAgain)
        {
            Console.WriteLine($"Total attempt left {attemptLeft}");
            Console.WriteLine("Enter your Pincode");
            string pinCodeInput = Console.ReadLine();
            attemptLeft--;

            // match pincode
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
        // Explicit casting to AccountType
        AccountType type = (AccountType)Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter your currency type, 0 for Euro, 1 for SEK");
        // Explicit casting to CurrencyType
        CurrencyType currencyType = (CurrencyType)Convert.ToInt32(Console.ReadLine());

        // generate 4 digit pincode randomly
        Random random = new Random();
        string pinCode = random.Next(1000, 9999).ToString();

        // Add an account to user's BankAccounts List
        accounts.Add(new BankAccount(userId, 0, type, pinCode, currencyType));

    }

    public static void TransferToOtherUserAccount(User[] users, User loggedInUser)
    {
       // Exception handling to prevent program crashing 
        try
        {
            Console.WriteLine("Enter your account number from where you want to transfer");

            for (int i = 0; i < loggedInUser.BankAccounts.Count; i++)
            {
                Console.WriteLine($"Enter {i + 1} for AccountNumber {loggedInUser.BankAccounts[i].AccountNumber}");
            }
            Int32.TryParse(Console.ReadLine(), out int transferFrom);

            BankAccount senderAccount = loggedInUser.BankAccounts[transferFrom - 1];

            Console.WriteLine("Enter 10 digit account number where you want to transfer, for example 1234567890");
            string receiverAccountNumber = Console.ReadLine();

            // Selecting all accounts from all users using selectmany and then get receiver account 
            BankAccount receiverAccount = users.SelectMany(o => o.BankAccounts).Where(account=>account.AccountNumber==receiverAccountNumber).FirstOrDefault();

            if (receiverAccount != null)
            {
                Console.WriteLine($"Enter amount to transfer, current balance is {senderAccount.Balance} {senderAccount.CurrencyType}");
                Double.TryParse(Console.ReadLine(), out Double amount);

                Double amountToDeposit = 0;

                // Currency Conversion and assuming following conversation rate
                // 1 Euro = 10 SEK
                // 1 SEK = 0.1 Euro
                if (senderAccount.CurrencyType == CurrencyType.Euro && receiverAccount.CurrencyType == CurrencyType.SEK)
                {
                    amountToDeposit = amount * 10;

                }
                else if (senderAccount.CurrencyType == CurrencyType.SEK && receiverAccount.CurrencyType == CurrencyType.Euro)
                {
                    amountToDeposit = amount / 10;
                }
                else
                {
                    amountToDeposit = amount;
                }

                // Check sender and receiver account owner is not same.
                if (senderAccount.OwnerId != receiverAccount.OwnerId)
                {
                    // Actual transfer by calling accounts withdrawal and deposite mehtods
                    senderAccount.MakeWithdrawal(amount, DateTime.Now, $"Transfer to another account {receiverAccount.AccountNumber}");
                    receiverAccount.MakeDeposit(amountToDeposit, DateTime.Now, $"New deposit from account {senderAccount.AccountNumber}");
                    Console.WriteLine("Successfully transfered amount to another user account");
                }
                else
                {
                    Console.WriteLine("Operation cancelled because you are trying to transfer amount to your own account. Please choose option number 2 from the menu");
                }
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Transfer failed! \n{ex}");
        }

    }     
}








