using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common.Abstractions.Entities;
using Common.Abstractions.Repository;

namespace Common.DataAccess
{
    public class GenericRepository : IAsyncGenericRepository
    {
        private readonly IDataSource _dataSource;
        private bool _disposed;

        public GenericRepository(IDataSource dataSource)
        {
            this._dataSource = dataSource;
        }

        public IQueryable<TEntity> GetAll<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class, IAggregateRoot
        {
            return this._dataSource.GetResultList(includeProperties);
        }

        public async Task<IEnumerable<TEntity>> GetByComplexExpressionAsync<TEntity>(
            Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>> expression,
            params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class, IAggregateRoot
        {
            return await expression.Compile().Invoke(this._dataSource.GetResultList(includeProperties)).ToListAsync();
        }

        public async Task<IEnumerable<TOut>> GetByComplexExpressionAndModifyAsync<TEntity, TOut>(
            Expression<Func<IQueryable<TEntity>, IQueryable<TOut>>> expression,
            params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class, IAggregateRoot
        {
            return await expression.Compile().Invoke(this._dataSource.GetResultList(includeProperties)).ToListAsync();
        }

        public IQueryable<TEntity> Find<TEntity>(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class, IAggregateRoot
        {
            return this._dataSource.GetResultList(includeProperties).Where(predicate);
        }

        public virtual TEntity FindSingle<TEntity>(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class, IAggregateRoot
        {
            return this._dataSource.GetResultSingle(predicate.Compile(), includeProperties);
        }

        public async Task<TEntity> FindSingleAsync<TEntity>(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class, IAggregateRoot
        {
            return await this._dataSource.GetResultSingleAsync(predicate, includeProperties);
        }

        public void Add<TEntity, TId>(TEntity entity) where TEntity : class, IAggregateRoot
        {
            this._dataSource.Add<TEntity, TId>(entity);
        }

        public void AddAndSave<TEntity, TId>(TEntity entity) where TEntity : class, IAggregateRoot
        {
            this.Add<TEntity, TId>(entity);
            this._dataSource.SaveChanges();
        }

        public virtual async Task AddAndSaveAsync<TEntity, TId>(TEntity entity) where TEntity : class, IAggregateRoot
        {
            this._dataSource.Add<TEntity, TId>(entity);
            await this._dataSource.SaveChangesAsync();
        }

        public void Update<TEntity, TId>(TEntity entity)
            where TEntity : class, IHaveId<TId>, IAggregateRoot
            where TId : struct
        {
            try
            {
                this._dataSource.Update<TEntity, TId>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateAndSave<TEntity, TId>(TEntity entity)
            where TEntity : class, IHaveId<TId>, IAggregateRoot
            where TId : struct
        {
            this.Update<TEntity, TId>(entity);
            this._dataSource.SaveChanges();
        }

        public async Task UpdateAndSaveAsync<TEntity, TId>(TEntity entity)
            where TEntity : class, IHaveId<TId>, IAggregateRoot
            where TId : struct
        {
            try
            {
                this.Update<TEntity, TId>(entity);
                await this._dataSource.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete<TEntity, TId>(TEntity entity)
            where TEntity : class, IHaveId<TId>, IAggregateRoot
            where TId : struct
        {
            this._dataSource.Remove<TEntity, TId>(entity);
        }

        public void DeleteAndSave<TEntity, TId>(TEntity entity)
            where TEntity : class, IHaveId<TId>, IAggregateRoot
            where TId : struct
        {
            this.Delete<TEntity, TId>(entity);
            this._dataSource.SaveChanges();
        }

        public async Task DeleteAndSaveAsync<TEntity, TId>(TEntity entity)
            where TEntity : class, IHaveId<TId>, IAggregateRoot
            where TId : struct
        {
            this.Delete<TEntity, TId>(entity);
            await this._dataSource.SaveChangesAsync();
        }

        public void DeleteById<TEntity, TId>(TId id)
            where TEntity : class, IHaveId<TId>, IAggregateRoot, new()
            where TId : struct
        {
            this._dataSource.Remove<TEntity, TId>(id);
        }

        public void DeleteByIdAndSave<TEntity, TId>(TId id)
            where TEntity : class, IHaveId<TId>, IAggregateRoot, new()
            where TId : struct
        {
            this.DeleteById<TEntity, TId>(id);
            this._dataSource.SaveChanges();
        }

        public virtual async Task DeleteByIdAndSaveAsync<TEntity, TId>(TId id)
            where TEntity : class, IHaveId<TId>, IAggregateRoot, new()
            where TId : struct
        {
            this.DeleteById<TEntity, TId>(id);
            await this._dataSource.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return this._dataSource.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this._dataSource.SaveChangesAsync();
        }

        public T ExecuteQueryResultSingle<T>(string query, int timeout = 30, params object[] parameters)
        {
            return this._dataSource.ExecuteQueryResultSingle<T>(query, timeout, parameters);
        }

        public IQueryable<T> ExecuteQueryResultList<T>(string query, params object[] parameters)
        {
            return this._dataSource.ExecuteQueryResultList<T>(query, parameters);
        }

        public void Dispose()
        {
            if (!this._disposed)
            {
                this._dataSource?.Dispose();
            }

            this._disposed = true;
        }
    }
}