using Library.Domain.Db;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Infrastructure
{
    public class RepositoryBase<T> where T : class
    {
        private LibraryDBContext dataContext;
        private readonly IDbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected LibraryDBContext DbContext
        {
            get
            {
                return dataContext ?? (dataContext = DbFactory.Init());

            }
        }

        public RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }

        #region Implementation

        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteAll()
        {
            IEnumerable<T> objects = dbSet.AsEnumerable();
            foreach (T obj in objects)
            {
                dbSet.Remove(obj);
            }
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
            {
                dbSet.Remove(obj);
            }
        }

        public virtual T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual T GetById2(Guid id)
        {
            return dbSet.Find(id);
        }
        public virtual IQueryable<T> GetAll()
        {
            return dbSet;
        }

        public virtual IQueryable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where);
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.AsNoTracking().Where(where).FirstOrDefault<T>();
        }

        public T GetNoTracking(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault<T>();
        }

        public virtual DbRawSqlQuery<T> GetWithRawSql(string query,
        params object[] parameters)
        {
            return dataContext.Database.SqlQuery<T>(query, parameters);
        }
        #endregion
    }
}
