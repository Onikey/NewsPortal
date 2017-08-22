using Common.Abstractions;
using Common.Abstractions.Entities;
using System;

namespace NewsPortal.Core.Model
{
    public class Author : Entity<Guid>, IAggregateRoot
    {
        public Author(Guid id)
            : base(id)
        {
        }

        protected Author()
            : base(Guid.NewGuid()) // required for EF
        {
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        protected DateTime BirthDate { get; set; }

        public static Author Create(string firstName, string lastName, DateTime birthDate)
        {
            var result = new Author
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate
            };

            return result;
        }
    }
}
