using ACEPSMVC.DataAccess.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ACEPSMVC.DataAccess.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        internal DbSet<T> dbSet;
        public Repository(DbContext context)
        {
            Context = context;
            this.dbSet = Context.Set<T>();
        }

        void IRepository<T>.Add(T entity)
        {
            dbSet.Add(entity);
        }

        T IRepository<T>.Get(int id)
        {
            return dbSet.Find(id);
        }

        IEnumerable<T> IRepository<T>.GetAll(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> , string includeProperties)
        {
            throw new NotImplementedException();
        }

        T IRepository<T>.GetFirstOrDefault(Expression<Func<T, bool>> filter, string includeProperties)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            T entityToRemove = dbSet.Find(id);
            dbSet.Remove(entityToRemove);
        }

        void IRepository<T>.Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
