using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected Context BD;

        public virtual TEntity GetById(int Id)
        {
            BD = new Context();
            return BD.Set<TEntity>().Find(Id);
        }

        public virtual TEntity GetFirst(Expression<Func<TEntity, bool>> Predicate = null)
        {
            BD = new Context();
            if (Predicate != null)
                return BD.Set<TEntity>().FirstOrDefault(Predicate);
            else
                return BD.Set<TEntity>().FirstOrDefault();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> Predicate = null)
        {
            BD = new Context();
            if (Predicate != null)
                return BD.Set<TEntity>().Where(Predicate);
            else
                return BD.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            BD = new Context();
            return BD.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> Predicate)
        {
            BD = new Context();
            return BD.Set<TEntity>().Where(Predicate);
        }

        public virtual TEntity Add(TEntity Entity)
        {
            BD = new Context();
            BD.Set<TEntity>().Add(entity: Entity);
            BD.SaveChanges();
            return Entity;
        }

        public virtual List<TEntity> AddAll(List<TEntity> List)
        {
            BD = new Context();
            BD.Set<TEntity>().AddRange(List);
            foreach (var item in List)
                BD.Entry(item).State = EntityState.Added;
            BD.SaveChanges();
            return List;
        }

        public virtual TEntity Attach(TEntity Entity)
        {
            BD = new Context();
            BD.Set<TEntity>().Update(entity: Entity);

            BD.SaveChanges();
            return Entity;
        }

        public virtual List<TEntity> AttachAll(List<TEntity> List)
        {
            BD = new Context();
            BD.Set<TEntity>().UpdateRange(List);

            BD.SaveChanges();
            return List;
        }

        public void RemoveById(int Id)
        {
            BD = new Context();
            var Entity = GetById(Id);
            BD.Set<TEntity>().Remove(entity: Entity);
            BD.SaveChanges();
        }

        public void Remove(TEntity Entity)
        {
            BD = new Context();
            BD.Set<TEntity>().Remove(entity: Entity);
            BD.SaveChanges();
        }

        public void Remove(Expression<Func<TEntity, bool>> Predicate)
        {
            BD = new Context();
            BD.Set<TEntity>().RemoveRange(GetList(Predicate));
            BD.SaveChanges();
        }

        public virtual void RemoveAll(List<TEntity> List)
        {
            BD = new Context();

            foreach (var item in List)
            {
                BD.Entry(item).State = EntityState.Deleted;
            }

            BD.Set<TEntity>().RemoveRange(List);
            BD.SaveChanges();
        }

        public void Dispose()
        {
            if (BD != null)
                BD.Dispose();
        }
    }
}