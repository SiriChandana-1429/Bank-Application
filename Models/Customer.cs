using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Customer:User
    {
       
        
        
        public Customer(string name, string accountId, string bankName, string bankId, string userId, string password, string email)
        {
            this.AccountId = accountId;
            this.BankName = bankName;
            this.BankId = bankId;
            this.UserId = userId;
            this.Password = password;
            this.Email = email;
            TypeOfUser = UserType.Customer;
        }






    }
}
