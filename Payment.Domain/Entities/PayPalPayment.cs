using System;
using Payment.Domain.Enums;
using Payment.Domain.ValueObjects;

namespace Payment.Domain.Entities
{
    public class PayPalPayment : Payment
    {
        public PayPalPayment(string number, DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, EDocumentType document, string payer, Address address, Email email, string transactionCode)
            : base(number, paidDate, expireDate, total, totalPaid, document, payer, address, email)
        {
            TransactionCode = transactionCode;
        }

        public string TransactionCode { get; private set; }
    }
}