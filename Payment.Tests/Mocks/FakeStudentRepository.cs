using Payment.Domain.Entities;
using Payment.Domain.Repositories;

namespace Payment.Tests.Mocks
{
    public class FakeStudentRepository : IStudentRepository
    {
        public void CreateSubscription(Student student)
        {
            throw new System.NotImplementedException();
        }

        public bool DocumentExists(string document)
        {
            if (document == "99999999999")
                return true;

            return false;
        }

        public bool EmailExistis(string email)
        {
            if (email == "hello@balta.io")
                return true;

             return false;
        }
    }
}
