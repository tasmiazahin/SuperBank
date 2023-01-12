# SuperBank
## Create an internet bank console application for multiple users.

The goals of the project is to create a simple internet banking console application where
multiple users can use the app. An individual user can have multiple accounts and can perform
different types of operation as needed.

SuperBank allows a user to perform following operations after successful login.

### Functionalities

* See your accounts and balance
* Transfer amount between own accounts
* Withdraw money
* Deposit money
* Open a new account
* Transfer amount between different user account
* Log out
* Terminate the program (won't be necessary)
 
### Thought process 
I have tried to emphasis following areas during planning and implementation phases which are;

* Data model
* Security
* Object Oriented Programming
* Exception handling
* Scalability of the application
* Program readibility

Data model has been structured in a way where a user can have list of bank accounts and a bank account will have list of
transactions. Balance of the account will be calculated from all transactions occured. UserId/OwnerId is introduced to establish
relationship between acount and user.

From the begnning, I thougt to use only password to authenticate to the system. Then I realise every bankaccount can have their
own card and might attach with pincode. So that, my application include multuple layer of security. An special feature is implemented where if user
enter 3 consecutive wrong PinCode system will autometacally detect that lock the operation for 3 minutes. 

1. ```Password``` for user login
2. account ```PinCode``` to make transaction

I have also added exceptional handling using  ```try/catch``` to prevent crashing the program.



### Extend the application in future  

* Since data model is created to keep all the trasaction history,it would nice to see all the transation happens to a specific account.
* Retry pincode/password can be enforce everywhere. Currently feature is implemented few places.
* Lock user can be added.
* Adding more validation would be nice as well.
* Saving all data to a data store/ database
