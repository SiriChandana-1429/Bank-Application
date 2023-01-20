using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Transaction
    {
        public string TransactionId { get; set; }
        public double SentAmount { get; set; }
        public double RecievedAmount { get; set; }
        public Customer Reciever { get;set; }
        public Customer Sender { get; set; }



        public Transaction(Customer sender, Customer reciever,double sentAmount,double recievedAmount,string transactionId)
        {
            this.Sender=sender;
            this.Reciever = reciever;
            this.SentAmount= sentAmount;
            this.RecievedAmount= recievedAmount;
            this.TransactionId = transactionId;
        }

        public Transaction(Customer sender,double amount,Customer reciever,string transactionId)
        {
            this.Sender=sender;
            this.Reciever= reciever;
            this.SentAmount= amount;
            this.RecievedAmount =amount;
            this.TransactionId = transactionId;
        }

        


    }
}
