using Payment.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Payment.Domain.Queries
{
    public static class StudentQueries
    {
        public static Expression<Func<Student, bool>> GetStudentInfo(string document)
        {
            return x => x.Document.Number == document;
        }
    }
}
