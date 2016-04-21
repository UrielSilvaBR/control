using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Control.Model.Entities
{
    [DataContract]
    public class Transaction : IEntity
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        public string TransType { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public string TransactionID { get; set; }

        public string Name { get; set; }

        //public DateTime? TransactionInitializationDate { get; set; }

        //public DateTime? FundAvaliabilityDate { get; set; }

        public string Memo { get; set; }

        public string IncorrectTransactionID { get; set; }

        public string TransactionCorrectionAction { get; set; }

        public string ServerTransactionID { get; set; }

        public string CheckNum { get; set; }

        public string ReferenceNumber { get; set; }

        public string Sic { get; set; }

        public string PayeeID { get; set; }

        //public Account TransactionSenderAccount { get; set; }

        public string Currency { get; set; }
    }
}
