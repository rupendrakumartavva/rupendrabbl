using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Common
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        IQueryable<T> FindBySingle(Expression<Func<T, bool>> predicate);

        //IEnumerable<T> GetBy<U>(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> columns);,IDisposable
        T GetFirstOrDefault(
       Expression<Func<T, bool>> filter = null,
       params Expression<Func<T, object>>[] includes);

        T Add(T entity);

        T Delete(T entity);

        T Update(T entity, int key);

        T Update(T entity, string key);

        void Edit(T entity);

        void Save();
    }
}