using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Transaction
    {
        [Key]
        public string TransactionId { get; set; }
        public float SentAmount { get; set; }
        public float RecievedAmount { get; set; }
        [ForeignKey("Account")]
        public string RecieverId { get; set; }

        [ForeignKey("Account")]
        public string SenderId { get; set; }
        


        public Transaction(string senderId, string recieverId,float sentAmount,float recievedAmount,string transactionId)
        {
            this.SenderId=senderId;
            this.RecieverId = recieverId;
            this.SentAmount= sentAmount;
            this.RecievedAmount= recievedAmount;
            this.TransactionId = transactionId;
        }

        public Transaction(string senderId,float amount,string recieverId,string transactionId)
        {
            this.SenderId=senderId;
            this.RecieverId= recieverId;
            this.SentAmount= amount;
            this.RecievedAmount =amount;
            this.TransactionId = transactionId;
        }

        


    }
}
