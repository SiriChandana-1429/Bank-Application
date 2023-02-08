using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models

{
    public enum UserType
    {
        Customer,
        Staff
    }
    public class User
    {

        public string UserId { get; set; }   
        public string Password { get; set; }
        public string BankId { get; set; }
        public string BankName { get; set; }
        public string AccountId { get; set; }
        public string Email { get; set; }
        public UserType TypeOfUser { get; set; }

        public User() { }
        public User(string userId,string password,string bankId,string bankName)
        {
            this.AccountId = "";
            this.UserId=userId;
            this.Password=password;
            this.BankId=bankId;
            this.BankName=bankName;
            this.TypeOfUser = UserType.Staff;

        }
        public User(string name, string accountId, string bankName, string bankId, string userId, string password, string email)
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
