using System;
using Payment.Domain.Enums;
using Payment.Domain.ValueObjects;

namespace Payment.Domain.Entities
{
    public class CreditCardPayment : Payment
    {
        public CreditCardPayment(string number, DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, EDocumentType document, string payer, Address address, Email email, string cardHoldName, string cardNumber, string lastTransactionNumber)
        : base(number, paidDate, expireDate, total, totalPaid, document, payer, address, email)
        {
            CardHoldName = cardHoldName;
            CardNumber = cardNumber;
            LastTransactionNumber = lastTransactionNumber;
        }

        public string CardHoldName { get; private set; }
        public string CardNumber { get; private set; }
        public string LastTransactionNumber { get; private set; }
    }
}