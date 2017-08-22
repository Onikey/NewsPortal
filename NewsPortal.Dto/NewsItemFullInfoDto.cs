using System;

namespace NewsPortal.Dto
{
    public class NewsItemFullInfoDto : IDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public Guid AuthorId { get; set; }
    }
}
