using System;

namespace NewsPortal.Dto
{
    public class AuthorShortInfoDto : IDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
