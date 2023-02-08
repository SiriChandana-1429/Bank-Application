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
            bankDBContext.Banks.Add(newBank);
            Console.WriteLine(newBank.BankId);
            Console.WriteLine(bankDBContext.Banks.Where(i => i.BankId.Equals(newBank.BankId)).ElementAt(0).BankName);
        }
        public static int CreateStaff(string bankName, string userName, string password)
        {
            var currentBank = bankDBContext.Banks.Where(i => i.BankName.Equals(bankName));
            if(!currentBank.Any() )
            {
                return 0;
            }
            else
            {
                DateTime now = DateTime.Now;
                string accountId = userName.Substring(0, 3) + now.ToString("");
                User user = new User(accountId, password, currentBank.ElementAt(0).BankId, bankName);
                bankDBContext.Users.Add(user);
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