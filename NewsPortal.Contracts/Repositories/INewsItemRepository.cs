using NewsPortal.Core.Model;
using NewsPortal.Dto;
using System;
using System.Collections.Generic;

namespace NewsPortal.Contracts.Repositories
{
    public interface INewsItemRepository : IDisposable
    {
        void Add(NewsItem item);

        ICollection<T> GetAllAs<T>() where T : class, IDto, new();

        ICollection<NewsItem> GetAll();

        NewsItem GetById(Guid id);

        T GetByIdAs<T>(Guid id) where T : class, IDto, new();

        void Update(NewsItem item);

        void Delete(NewsItem item);
    }
}
