﻿using Payment.Domain.Services;

namespace Payment.Tests.Mocks
{
    class FakeEmailService : IEmailService
    {
        public void Send(string to, string email, string subject, string body)
        {
            
        }
    }
}
