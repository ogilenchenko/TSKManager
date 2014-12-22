using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TM.Database
{
    public class EfGenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public EfGenericRepository(DbSet<T> dbSet)
        {
            _dbSet = dbSet;
        }

        #region IGenericRepository<T> implementation

        public virtual IQueryable<T> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public IEnumerable<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public async Task<T> FindAsync(string keyValue)
        {
            return await _dbSet.FindAsync(keyValue);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate, string[] includes)
        {
            foreach (var include in includes)
            {
                _dbSet.Include(include);
            }

            return _dbSet.Where(predicate);
        }

        public T Single(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Single(predicate);
        }

        public T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.SingleOrDefault(predicate);
        }

        public T First(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.First(predicate);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T GetById(string id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            _dbSet.Remove(entity);
        }

        public void Attach(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            _dbSet.Attach(entity);
        }

        #endregion
    }
}
