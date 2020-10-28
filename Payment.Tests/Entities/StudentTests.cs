using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment.Domain.Entities;
using Payment.Domain.ValueObjects;
using Payment.Domain.Enums;
using System;

namespace Payment.Tests
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Email _email;
        private readonly Student _student;
        private readonly Subscription _subscription;

        public StudentTests()
        {
            _name = new Name("Bruce", "Wayne");
            _document = new Document("12345678912", EDocumentType.CPF);
            _email = new Email("m@m.com");
            _address = new Address("Main Street", "30", "Centro", "Gotham", "DC", "EUA", "06445000");
            _student = new Student(_name, _document, _email);
            _subscription = new Subscription(null);
            

        }

        [TestMethod]
        public void ShouldReturnErrorWhenAlreadyHadActiveSubscription()
        {
            var payment = new PayPalPayment("123456", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, EDocumentType.CNPJ, "Wayne CORP", _address, _email, "123456");
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }

        public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
        {
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription()
        {
            var subscription = new Subscription(null);
            var payment = new PayPalPayment("123456", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, EDocumentType.CNPJ, "Wayne CORP", _address, _email, "123456");
            subscription.AddPayment(payment);
            _student.AddSubscription(subscription);

            Assert.IsTrue(_student.Valid);
        }
    }
}