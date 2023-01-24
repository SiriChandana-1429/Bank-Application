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

        public string Name { get; set; }   
        public string Password { get; set; }
        public string BankId { get; set; }
        public string BankName { get; set; }
        public string AccountId { get; set; }
        public string Email { get; set; }
        public UserType TypeOfUser { get; set; }

        

    }
}
