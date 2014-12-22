using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TM.Database
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> AsQueryable();

        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate, string[] includes);

        Task<T> FindAsync(string keyValue);
        
        T Single(Expression<Func<T, bool>> predicate);
        T SingleOrDefault(Expression<Func<T, bool>> predicate);
        T First(Expression<Func<T, bool>> predicate);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        T GetById(int id);
        T GetById(string id);

        void Add(T entity);
        void Delete(T entity);
        void Attach(T entity);
    }
}
