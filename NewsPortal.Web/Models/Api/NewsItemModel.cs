using System;

namespace NewsPortal.Web.Models.Api
{
    public class NewsItemModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public Guid AuthorId { get; set; }
    }
}