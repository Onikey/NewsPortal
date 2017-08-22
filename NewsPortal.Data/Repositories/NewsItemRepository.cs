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
    public class NewsItemRepository : INewsItemRepository
    {
        private readonly IAsyncGenericRepository _repository;

        public NewsItemRepository(IAsyncGenericRepository genericRepository)
        {
            this._repository = genericRepository;
        }

        public void Add(NewsItem item)
        {
            this._repository.AddAndSave<NewsItem, Guid>(item);
        }

        public ICollection<T> GetAllAs<T>() where T : class, IDto, new() 
        {
            return this._repository.GetAll<NewsItem>(x => x.Author).ProjectTo<T>().ToList();
        }

        public ICollection<NewsItem> GetAll()
        {
            return this._repository.GetAll<NewsItem>(x => x.Author).ToList();
        }

        public NewsItem GetById(Guid id)
        {
            return this._repository.FindSingle<NewsItem>(x => x.Id == id);
        }

        public T GetByIdAs<T>(Guid id) where T : class, IDto, new()
        {
            return this._repository.Find<NewsItem>(x => x.Id == id).ProjectTo<T>().Single();
        }

        public void Update(NewsItem item)
        {
            this._repository.UpdateAndSave<NewsItem, Guid>(item);
        }

        public void Delete(NewsItem item)
        {
            this._repository.DeleteAndSave<NewsItem, Guid>(item);
        }

        public void Dispose()
        {
            this._repository.Dispose();
        }
    }
}
