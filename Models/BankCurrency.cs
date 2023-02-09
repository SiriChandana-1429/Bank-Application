using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BankCurrency
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string BankId { get; set; }
        public BankCurrency(string name,string bankId)
        { 
            this.Name= name;
            this.BankId= bankId;
        }
    
    }
}
