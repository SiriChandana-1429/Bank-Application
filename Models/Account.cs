using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Account
    {

        [Key]
        public string AccountId { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        [Required]
        public float Balance { get; set; }

        public ICollection<Transaction> Transactions;

        
        public Account(string accountId,float balance,string userId)
        { 
            this.AccountId= accountId;
            this.Balance = balance;
            this.UserId= userId;
        }

    }
}
