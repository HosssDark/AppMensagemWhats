using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class
    {
        TEntity GetById(int Id);
        TEntity GetFirst(Expression<Func<TEntity, bool>> Predicate = null);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> Predicate = null);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> Predicate);

        TEntity Add(TEntity Entity);
        List<TEntity> AddAll(List<TEntity> List);

        TEntity Attach(TEntity Entity);
        List<TEntity> AttachAll(List<TEntity> List);

        void RemoveById(int Id);
        void Remove(TEntity Entity);
        void Remove(Expression<Func<TEntity, bool>> Predicate);
        void RemoveAll(List<TEntity> List);
    }
}