using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using BusinessLogic;
using DataAccessLayer;

BankDataBaseContext bankDbContext=new BankDataBaseContext();
while (true)
{
    Console.WriteLine("                                  *******====================*******");
    Console.WriteLine("                                         1.Account Holder");
    Console.WriteLine("                                         2.Bank Staff");
    Console.WriteLine("                                         3.Exit");
    Console.WriteLine("                                         4.Admin");
    string user = Console.ReadLine();
    if (user == "3") break;
    switch (user)
    {
        case "1":
            {
                Console.WriteLine("Enter Bank ID:");
                string bankId = Console.ReadLine();
                Console.WriteLine("Enter UserName:");
                string UserName = Console.ReadLine();
                Console.WriteLine("Enter Password:");
                string password = Console.ReadLine();

                var checkForCustomerBank = bankDbContext.Banks.Where(i => i.BankId.Equals(bankId));

                if (!checkForCustomerBank.Any())
                {
                    Console.WriteLine("Incorrect bank name given..!!");
                    break;
                }
                var checkForUser = checkForCustomerBank.ElementAt(0).Users.Where(i => (i.UserId.Equals(UserName) && (i.Password.Equals(password))));
                if (!checkForUser.Any())
                {
                    Console.WriteLine("User not found..!!");
                    break;
                }
                var checkForAccount = checkForCustomerBank.ElementAt(0).Accounts.Where(i => i.AccountId.Equals(checkForUser.ElementAt(0).AccountId));

                if (checkForAccount.Any())
                {
                    Console.WriteLine("                             1.Deposit Amount");
                    Console.WriteLine("                             2.Withdraw Amount");
                    Console.WriteLine("                             3.Transfer Funds");
                    Console.WriteLine("                             4.View Transaction History");
                    string AccountHolderChoice = Console.ReadLine();
                    switch (AccountHolderChoice)
                    {
                        case "1":
                            Console.WriteLine("                     Enter amount:");
                            string amount = Console.ReadLine();
                            foreach (Customer currentHolder in checkForUser)
                            {
                                int value = CustomerServices.DepositAmount(Convert.ToInt32(amount), checkForAccount.ElementAt(0).AccountId);
                                if (value == 0)
                                {
                                    Console.WriteLine("Please enter amount more than 0");
                                }
                                else
                                {
                                    Console.WriteLine("Updated Balance:{0}", checkForAccount.ElementAt(0).Balance);
                                }
                            }

                            break;
                        case "2":
                            Console.WriteLine("              Enter the amount you want to withdraw:");
                            string withDrawamount = Console.ReadLine();
                            foreach (Customer currentHolder in checkForUser)
                            {

                                int value = CustomerServices.WithDrawAmount(Convert.ToInt32(withDrawamount), checkForAccount.ElementAt(0).AccountId);
                                if (value == 0)
                                {
                                    Console.WriteLine("Insufficient Funds..!!");
                                }
                                else
                                {
                                    Console.WriteLine("Updated Balance: " + checkForAccount.ElementAt(0).Balance);
                                }
                            }


                            break;
                        case "3":
                            foreach (Customer currentHolder in checkForUser)
                            {

                                Console.WriteLine("Enter Reciever's Bank ID:");
                                string recieverBankID = Console.ReadLine();
                                Console.WriteLine("Enter Account ID to which you want to transfer funds:");
                                string transferAccount = Console.ReadLine();
                                var checkForReciever = bankDbContext.Users.Where(acc => (acc.TypeOfUser.Equals(UserType.Customer) && acc.AccountId.Equals(transferAccount)));
                                if (!checkForReciever.Any())
                                {
                                    Console.WriteLine("User not found..!!");
                                    break;
                                }
                                var checkForRecieverAccount = from acc in bankDbContext.Users
                                                              where acc.AccountId.Equals(checkForReciever.ToList()[0].AccountId)
                                                              select acc;
                                if (!checkForRecieverAccount.Any())
                                {
                                    Console.WriteLine("Account not found in current Bank..!!");
                                    break;
                                }
                                Console.WriteLine("Enter amount:");
                                string transferAmount = Console.ReadLine();

                                StaffServices.ValidateCustomer(recieverBankID, checkForRecieverAccount.ElementAt(0).AccountId);
                                Console.WriteLine("Which type of transaction do you want to initiate?");
                                Console.WriteLine("1.RTGS");
                                Console.WriteLine("2.IMPS");
                                string transferType = Console.ReadLine();
                                int value = CustomerServices.TransferFunds(checkForAccount.ToList()[0].AccountId,
                                                                            checkForRecieverAccount.ToList()[0].AccountId,
                                                                            Convert.ToInt32(transferAmount),
                                                                                transferType,
                                                                                checkForCustomerBank.ElementAt(0).BankId,
                                                                                checkForRecieverAccount.ElementAt(0).BankId);

                                if (value == 0)
                                {
                                    Console.WriteLine("Insufficient funds");
                                }
                                else
                                {
                                    Console.WriteLine("Updated Balance:{0}", checkForAccount.ToList()[0].Balance);

                                }
                            }
                            break;
                        case "4":

                            var transactionList = bankDbContext.Transactions.Where(i => (i.SenderId.Equals(checkForAccount.ElementAt(0).AccountId)) || (i.RecieverId.Equals(checkForAccount.ElementAt(0).AccountId)));
                            Console.WriteLine("             Transaction History");
                            Console.WriteLine("Sender ID                Sent Amount                Transaction ID                   Recieved Amount");
                            foreach (Transaction transaction in transactionList)

                                Console.WriteLine(transaction.SenderId + "             " + transaction.SentAmount + "             " + transaction.TransactionId + "             " + transaction.RecievedAmount + "             " + transaction.RecieverId);


                            break;

                    }
                    break;

                }
                else
                {
                    Console.WriteLine("Account Holder not found.!!");
                    break;
                }
            }
        case "2":
            {
                Console.WriteLine("                 Hi Staff!!");
                Console.WriteLine("Enter Bank ID:");
                string staffBank = Console.ReadLine();
                Console.WriteLine("Enter Staff User Name:");
                string staffUserName = Console.ReadLine();
                Console.WriteLine("Enter Password:");
                string staffPassword = Console.ReadLine();

                var checkForBankStaff = bankDbContext.Banks.Where(i => i.BankId.Equals(staffBank));
               
                if (!checkForBankStaff.Any())
                {
                    Console.WriteLine("Entered Bank ID not found..!!");
                    break;
                }
                var checkForStaff = checkForBankStaff.ElementAt(0).Users.Where(i => (i.UserId.Equals(staffUserName)) && (i.Password.Equals(staffPassword)));
                if (!checkForStaff.Any())
                {
                    Console.WriteLine("Incorrect credentials were given!!");
                }
                else
                {
                    foreach (User currentStaff in checkForStaff)
                    {

                        Console.Write("Your Bank ID is:");
                        Console.WriteLine(checkForBankStaff.ElementAt(0).BankId);
                        Console.WriteLine("--------------------*****------------------------");
                        Console.WriteLine("--------------------*****------------------------");
                        Console.WriteLine("            1.Create new Account");
                        Console.WriteLine("            2.Update/Delete account");
                        Console.WriteLine("            3.Add new accepted currency");
                        Console.WriteLine("            4.Add new service charges for same bank account");
                        Console.WriteLine("            5.Add new service charges for other bank account");
                        Console.WriteLine("            6.View account transaction");
                        Console.WriteLine("            7.Revert transaction");

                        string action = Console.ReadLine();
                        switch (action)
                        {
                            case "1":
                                Console.WriteLine("Enter First Name:");
                                string firstName = Console.ReadLine();
                                Console.WriteLine("Enter Last Name:");
                                string lastName = Console.ReadLine();
                                Console.WriteLine("Enter Email:");
                                string email = Console.ReadLine();
                                Console.WriteLine("Enter Password:");
                                string accountPassword = Console.ReadLine();
                                Console.WriteLine("Bank ID:");
                                string bankID = Console.ReadLine();
                                string returnValue = StaffServices.CreateAccount(checkForBankStaff.ElementAt(0).BankName, firstName, lastName, email, accountPassword, bankID, currentStaff.UserId);
                                if (returnValue == "0")
                                {
                                    Console.WriteLine("Invalid bank Id given.");
                                }
                                else
                                {
                                    Console.WriteLine("                         *Account created*");
                                    Console.WriteLine("                   AccountId:{0}", returnValue);
                                }
                                break;
                            case "2":
                                Console.WriteLine("Enter Account ID that you want to delete:");
                                string delAccount = Console.ReadLine();
                                int returnValueInt = StaffServices.DeleteAccount(delAccount, currentStaff.UserId);
                                if (returnValueInt == 1)
                                {
                                    Console.WriteLine("Account Deleted..!!");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid account ID given");
                                }
                                break;
                            case "3":
                                Console.WriteLine("Enter the new accepted currency:");
                                string newCurrency = Console.ReadLine();
                                Console.WriteLine("Enter the value of the new currency:");
                                int newCurrencyValue = Convert.ToInt32(Console.ReadLine());
                                returnValueInt = StaffServices.AddNewCurrency(newCurrency.ToUpper(), newCurrencyValue);
                                if (returnValueInt == 0)
                                {
                                    Console.WriteLine("Currency already exists");
                                }
                                else if (returnValueInt == 1)
                                {
                                    Console.WriteLine("Value should be greater than 0");
                                }
                                else
                                {
                                    Console.WriteLine("Currency added");
                                }

                                break;
                            case "4":
                                Console.WriteLine("Enter new RTGS Charges:");
                                float newRTGSCharges = float.Parse(Console.ReadLine());
                                Console.WriteLine("Enter new IMPS Charges:");
                                float newIMPSCharges = float.Parse(Console.ReadLine());
                                StaffServices.UpdateChargesForSameBank(checkForBankStaff.ElementAt(0).BankId, newRTGSCharges, newIMPSCharges);
                                break;
                            case "5":
                                Console.WriteLine("Enter new RTGS Charges:");
                                newRTGSCharges = float.Parse(Console.ReadLine());
                                Console.WriteLine("Enter new IMPS Charges:");
                                newIMPSCharges = float.Parse(Console.ReadLine());
                                StaffServices.UpdateChargesForOtherBank(checkForBankStaff.ElementAt(0).BankId, newRTGSCharges, newIMPSCharges);

                                break;
                            case "6":
                                Console.WriteLine("Enter account ID to view transaction:");
                                string viewAccountTransaction = Console.ReadLine();
                                var checkForBank = bankDbContext.Banks.Where(i => i.BankId.Equals(checkForBankStaff.ElementAt(0).BankId));

                                if (checkForBank == null)
                                {
                                    Console.WriteLine("This bank staff is not authorised to view this account transaction history..");
                                    break;
                                }

                                Console.WriteLine("SenderId                 Amount                 ReceiverID                  Transaction ID");
                                foreach (Transaction transaction in bankDbContext.Transactions)
                                {
                                    Console.WriteLine(transaction.SenderId + "                  " + transaction.SentAmount + "          " + transaction.RecieverId + "                      " + transaction.TransactionId);
                                }

                                break;
                            case "7":
                                Console.WriteLine("Enter the account Id whose transaction you want to revert?");
                                string senderAccountId = Console.ReadLine();
                                var checkForSender = checkForBankStaff.ElementAt(0).Accounts.Where(acc => acc.AccountId.Equals(senderAccountId));
                                if (!checkForSender.Any())
                                {
                                    Console.WriteLine("Invalid Sender ID given");
                                    break;
                                }
                                Console.WriteLine("Enter the reciever account Id you want to revert?");
                                string recieverAccountId = Console.ReadLine();
                                Console.WriteLine("Enter reciever's Bank ID:");
                                string reciverBankId = Console.ReadLine();
                                Console.WriteLine("Enter the transaction ID that you want to revert?");
                                string revertTransactionId = Console.ReadLine();

                                StaffServices.RevertTransaction(checkForSender.ElementAt(0).AccountId, revertTransactionId, currentStaff.UserId, checkForBankStaff.ElementAt(0).BankId);
                                break;

                            default:
                                Console.WriteLine("Invalid choice");
                                break;

                        }
                        break;
                    }
                }





                break;
            }
        case "4":
            {
                Console.WriteLine("                               *********==========********");
                Console.WriteLine("                                 1.Create Bank");
                Console.WriteLine("                                 2.Create Staff Account");
                Console.WriteLine("                                 3.Change default Currency");
                string adminChoice = Console.ReadLine();
                switch (adminChoice)
                {
                    case "1":
                        Console.WriteLine("Enter Bank Name:");
                        string newBankName = Console.ReadLine();
                        Console.WriteLine("Enter Bank Location:");
                        string bankLocation = Console.ReadLine();
                        Console.WriteLine("Enter Bank Currency:");
                        string currency = Console.ReadLine();
                        AdminServices.CreateBank(newBankName, bankLocation, currency);
                        Console.WriteLine("Bank is created successfully");
                        break;
                    case "2":
                        Console.WriteLine("Enter Bank Name:");
                        string newStaffBank = Console.ReadLine();
                        Console.WriteLine("Enter user name:");
                        string userName = Console.ReadLine();
                        Console.WriteLine("Enter password:");
                        string staffPassword = Console.ReadLine();
                        int returnValue = AdminServices.CreateStaff(newStaffBank, userName, staffPassword);
                        if (returnValue == 0)
                        {
                            Console.Write("Bank doesn't exists");
                        }
                        else
                        {
                            Console.WriteLine("Staff added.");
                        }
                        break;
                        //case "3":
                        //    Console.WriteLine("Enter Bank Name");
                        //    string currencyBank = Console.ReadLine();
                        //    Console.WriteLine("Enter new currency:");
                        //    string newCurrency = Console.ReadLine();
                        //    AdminServices.ChangeCurrency(newCurrency, currencyBank);
                        //    break;
                }


                break;
            }
        default:
            Console.WriteLine("                                 Invalid Choice");
            break;

    }


}
