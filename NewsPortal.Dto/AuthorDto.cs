using System;

namespace NewsPortal.Dto
{
    public class AuthorDto : IDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
