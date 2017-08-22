using NewsPortal.Core.Model;
using NewsPortal.Dto;
using System;
using System.Collections.Generic;

namespace NewsPortal.Contracts.Repositories
{
    public interface IAuthorRepository : IDisposable
    {
        void Add(Author item);

        ICollection<T> GetAllAs<T>() where T : class, IDto, new();

        ICollection<Author> GetAll();

        Author GetById(Guid id);

        void Update(Author item);

        void Delete(Author item);
    }
}
