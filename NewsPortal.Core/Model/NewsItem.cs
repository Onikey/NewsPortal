using Common.Abstractions;
using Common.Abstractions.Entities;
using System;

namespace NewsPortal.Core.Model
{
    public class NewsItem : Entity<Guid>, IAggregateRoot//, IHaveHistory
    {
        public NewsItem(Guid id)
            : base(id)
        {
        }

        protected NewsItem()
            : base(Guid.NewGuid()) // required for EF
        {
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreationDate { get; protected set; }

        public DateTime LastModificationDate { get; protected set; }

        public Guid AuthorId { get; set; }

        public Author Author { get; protected set; }

        public static NewsItem Create(string title, string content, Author author)
        {
            var result = new NewsItem
            {
                Title = title,
                Content = content,
                AuthorId = author.Id
            };

            return result;
        }
    }
}
