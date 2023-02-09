using DataAccessLayer;
using Models;

namespace BusinessLogic
{

    public class AdminServices
    {

        static BankDataBaseContext bankDBContext=new BankDataBaseContext();
        public static void CreateBank(string bankName, string bankLocation, string currency)
        {

            Bank newBank = new Bank(bankName, bankLocation, currency);
            bankDBContext.BankCurrencies.Add(new BankCurrency("INR",newBank.BankId));
            bankDBContext.BankCurrencies.Add(new BankCurrency("USD", newBank.BankId));
            bankDBContext.BankCurrencies.Add(new BankCurrency("EUR", newBank.BankId));
            bankDBContext.SaveChanges();
            bankDBContext.Banks.Add(newBank);
            bankDBContext.SaveChanges();
            
            Console.WriteLine(newBank.BankId);
            
        }
        public static int CreateStaff(string bankName, string userName, string password,string email)
        {
            List<Bank> currentBank = bankDBContext.Banks.Where(i => i.BankName.Equals(bankName)).ToList();
            if(!(currentBank.Count()>0) )
            {
                return 0;
            }
            else
            {
                DateTime now = DateTime.Now;
                string accountId = userName.Substring(0, 3) + now.ToString("");
                User user = new User(accountId, password, currentBank[0].BankId, bankName,email);
                bankDBContext.Users.Add(user);
                bankDBContext.SaveChanges();
                return 1;
            }
            

        }
        //public static void ChangeCurrency(string currency, string bankName)
        //{

        //    foreach (var bank in BankRepository.AllBanks)
        //    {
        //        if (bank.Key == bankName)
        //        {
        //            bank.Value.Currency = currency;
        //        }
        //    }

        //}


    }
}