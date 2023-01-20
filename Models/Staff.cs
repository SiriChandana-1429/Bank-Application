using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Staff:User
    {
       
        
        public Staff(string userName, string password, string bankId, string bankName)
        {
            this.UserName = userName;
            this.Password = password;
            this.BankId = bankId;
            this.BankName = bankName;


        }

    }
}
