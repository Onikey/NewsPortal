namespace NewsPortal.Dto
{
    public class NewsItemDto : IDto
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public AuthorDto Author { get; set; }
    }
}
