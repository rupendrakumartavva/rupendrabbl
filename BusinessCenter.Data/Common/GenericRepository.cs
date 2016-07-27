using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Common
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
    where T : class
    {
        protected DbContext Entities;
        protected DbSet<T> DbSet;
       // private bool _disposed;

        /// <summary>
        /// Initializes a new Instance of Generic Repository With Specified Generic Entity
        /// </summary>
        /// <param name="context">Unit of work object</param>
        protected GenericRepository(IUnitOfWork context)
        {
            Entities = context.getContext();
            DbSet = Entities.Set<T>();
        }
        /// <summary>
        /// This Method is used to Update using Generic Repository using long key value
        /// </summary>
        /// <param name="updated"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual T Update(T updated, long key)
        {
            if (updated == null)
                return null;

            T existing = Entities.Set<T>().Find(key);
            if (existing != null)
            {
                Entities.Entry(existing).CurrentValues.SetValues(updated);
            }
            return existing;
        }
        /// <summary>
        /// This Method is used to Update using Generic Repository using integer key value
        /// </summary>
        /// <param name="updated"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual T Update(T updated, int key)
        {
            if (updated == null)
                return null;

            T existing = Entities.Set<T>().Find(key);
            if (existing != null)
            {
                Entities.Entry(existing).CurrentValues.SetValues(updated);
            }
            return existing;
        }
        /// <summary>
        /// This Method is used to Update using Generic Repository using String Key value
        /// </summary>
        /// <param name="updated"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual T Update(T updated, string key)
        {
            if (updated == null)
                return null;

            T existing = Entities.Set<T>().Find(key);
            if (existing != null)
            {
                Entities.Entry(existing).CurrentValues.SetValues(updated);
            }
            return existing;
        }

        /// <summary>
        /// Gets all the records of T type Entity
        /// </summary>
        /// <returns>IEnumerable of records</returns>
        public virtual IEnumerable<T> GetAll()
        {
            return DbSet.AsEnumerable<T>();
        }
        /// <summary>
        /// This method is used to Get the values for the particular condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> query = DbSet.Where(predicate).AsEnumerable();
            return query;
        }
        /// <summary>
        /// This method is used to Get the values for the particular condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<T> FindBySingle(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = DbSet.Where(predicate).AsQueryable().AsNoTracking();
            return query;
        }

        /// <summary>
        /// Adds a particular Record to the DbSet Object(Database)
        /// </summary>
        /// <param name="entity">Entity type object which has to be inserted into database</param>
        /// <returns></returns>
        public virtual T Add(T entity)
        {
            return DbSet.Add(entity);
        }

        /// <summary>
        /// Deletes the particular Record from the database
        /// </summary>
        /// <param name="entity">Entity type object which has to be deleted from database</param>
        /// <returns></returns>
        public virtual T Delete(T entity)
        {
            return DbSet.Remove(entity);
        }
        /// <summary>
        /// Edit the particular Record from the database
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Edit(T entity)
        {
            Entities.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }
        /// <summary>
        /// Get the value using includes
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public virtual T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = DbSet;

            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include(include);

            return query.FirstOrDefault(filter);
        }

        /// <summary>
        /// Commits saved values to the database
        /// </summary>
        public virtual void Save()
        {
            Entities.SaveChanges();
        }
    }
}