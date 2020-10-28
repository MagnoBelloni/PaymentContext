using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment.Domain.Enums;
using Payment.Domain.ValueObjects;

namespace Payment.Tests
{
    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenCNPJIsInvalid()
        {
            var doc = new Document("123", EDocumentType.CNPJ);
            Assert.IsTrue(doc.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenCNPJInvalid()
        {
            var doc = new Document("12345678912345", EDocumentType.CNPJ);
            Assert.IsTrue(doc.Valid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenCPFIsInvalid()
        {
            var doc = new Document("", EDocumentType.CPF);
            Assert.IsTrue(doc.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenCPFInvalid()
        {
            var doc = new Document("12345678901", EDocumentType.CPF);
            Assert.IsTrue(doc.Valid);
        }
    }
}
