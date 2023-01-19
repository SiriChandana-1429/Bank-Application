using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Transaction
    {
        public string TransactionId;
        public string ReceiverId;
        public string BankId;
        public double SentAmount;
        public double RecievedAmount;
        public string ReceiverBankId;
        public string SenderAccountId;


        public Transaction(string transactionId, string senderAccountId, string bankId, double sentAmount, double recievedAmount)
        {
            this.TransactionId = transactionId;
            this.SenderAccountId = senderAccountId;
            this.BankId = bankId;
            this.SentAmount = sentAmount;
            this.RecievedAmount = recievedAmount;


        }

        public Transaction(string senderAccountId, double sentAmount, double recievedAmount, string bankId, string receiverId, string receiverBankId)
        {



            foreach (var checkBank in Admin.AllBanks)
            {
                if (checkBank.Value.BankId == receiverBankId)
                {
                    foreach (AccountHolder checkAccount in checkBank.Value.AllAccounts)
                    {
                        if (checkAccount.AccountId.Equals(receiverId))
                        {
                            this.SenderAccountId = senderAccountId;
                            this.TransactionId = "TXN" + bankId + senderAccountId;
                            this.SentAmount = sentAmount;
                            this.ReceiverId = receiverId;
                            this.ReceiverBankId = receiverBankId;
                            this.RecievedAmount = recievedAmount;
                            checkAccount.Balance += recievedAmount;
                            Transaction recieverTransactionObject = new Transaction(this.TransactionId, senderAccountId, bankId, sentAmount, recievedAmount);
                            checkAccount.Transactions.Add(recieverTransactionObject);


                            break;

                        }
                    }

                }
            }


        }


    }
}
