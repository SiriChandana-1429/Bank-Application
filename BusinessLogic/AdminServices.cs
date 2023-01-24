using Models;

namespace BusinessLogic
{

    public class AdminServices
    {


        public static void CreateBank(string bankName, string bankLocation, string currency)
        {

            Bank newBank = new Bank(bankName, bankLocation, currency);
            Admin.AllBanks.Add(bankName, newBank);

        }
        public static int CreateStaff(string bankName, string userName, string password)
        {

            foreach (var bank in Admin.AllBanks)
            {
                if (bank.Key == bankName)
                {

                    Staff newStaff = new Staff(userName, password, bank.Value.BankId, bankName);
                    bank.Value.AllUsers.Add(newStaff);
                    return 1;

                }

            }
            return 0;

        }
        public static void ChangeCurrency(string currency, string bankName)
        {

            foreach (var bank in Admin.AllBanks)
            {
                if (bank.Key == bankName)
                {
                    bank.Value.Currency = currency;
                }
            }

        }


    }
}