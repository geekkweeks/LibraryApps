using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        //Marks an entity as new
        void Add(T entity);
        //Marks an entity as modified
        void Update(T entity);
        //Marks an entity to be removed
        void Delete(T entity);

        void Delete(Expression<Func<T, bool>> where);
        //Get an entity by int id
        T GetById(int id);
        //Get an entity using delegate
        T GetById2(Guid id);
        T Get(Expression<Func<T, bool>> where);
        T GetNoTracking(Expression<Func<T, bool>> where);
        //Get all entities of type T
        IQueryable<T> GetAll();
        //Gets entitties using delegate
        IQueryable<T> GetMany(Expression<Func<T, bool>> where);
        DbRawSqlQuery<T> GetWithRawSql(string query,
       params object[] parameters);
        //Remove all data
        void DeleteAll();
    }
}
