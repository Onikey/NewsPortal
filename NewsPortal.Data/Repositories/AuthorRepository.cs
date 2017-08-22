using AutoMapper.QueryableExtensions;
using Common.Abstractions.Repository;
using NewsPortal.Contracts.Repositories;
using NewsPortal.Core.Model;
using NewsPortal.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsPortal.Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IAsyncGenericRepository _repository;

        public AuthorRepository(IAsyncGenericRepository genericRepository)
        {
            this._repository = genericRepository;
        }

        public void Add(Author item)
        {
            this._repository.AddAndSave<Author, Guid>(item);
        }

        public ICollection<T> GetAllAs<T>() where T : class, IDto, new()
        {
            return this._repository.GetAll<Author>(x => x.LastName).ProjectTo<T>().ToList();
        }

        public ICollection<Author> GetAll()
        {
            return this._repository.GetAll<Author>(x => x.LastName).ToList();
        }

        public Author GetById(Guid id)
        {
            return this._repository.FindSingle<Author>(x => x.Id == id);
        }

        public void Update(Author item)
        {
            this._repository.UpdateAndSave<Author, Guid>(item);
        }

        public void Delete(Author item)
        {
            this._repository.DeleteAndSave<Author, Guid>(item);
        }


        public void Dispose()
        {
            this._repository.Dispose();
        }
    }
}
