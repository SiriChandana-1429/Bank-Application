using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AcceptedCurrency
    {
        [Key]
        public string Name { get; set; }
        public float Value { get; set; }
        public AcceptedCurrency(string name,float value)
        { 
            this.Name= name;
            this.Value= value;
        }
        
        
    }
}
