using System;
using Payment.Domain.Enums;
using Payment.Domain.ValueObjects;

namespace Payment.Domain.Entities
{

    public class BoletoPayment : Payment
    {
        public BoletoPayment(string number, DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, EDocumentType document, string payer, Address address, Email email, string barCode, string boletoNumber)
            : base(number, paidDate, expireDate, total, totalPaid, document, payer, address, email)
        {
            BarCode = barCode;
            BoletoNumber = boletoNumber;
        }

        public string BarCode { get; private set; }
        public string BoletoNumber { get; private set; }
    }
}