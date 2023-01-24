using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Staff:User
    {

        int salary;
        public Staff(string name, string password, string bankId, string bankName)
        {
            this.Name = name;
            this.Password = password;
            this.BankId = bankId;
            this.BankName = bankName;
            TypeOfUser = UserType.Staff;
        }
        
    }
}
