using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Customer:User
    {
       
        
        //public Customer() { }
        public Customer(string name, string accountId, string bankName, string bankId, string userName, string password, string email)
        {
            this.AccountId = accountId;
            this.BankName = bankName;
            this.BankId = bankId;
            this.Name = name;
            this.Password = password;
            this.Email = email;
            TypeOfUser = UserType.Customer;
        }






    }
}
