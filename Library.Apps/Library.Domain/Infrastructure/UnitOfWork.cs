using Library.Domain.Db;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private LibraryDBContext dbContext;
        private readonly IDbFactory dbFactory;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public LibraryDBContext DbContext
        {
            get
            {
                return dbContext ?? (dbContext = dbFactory.Init());
            }
        }

        //public virtual void Commit()
        //{
        //    base.SaveChanges();
        //}

        public void Commit()
        {
            DbContext.Commit();
        }

        public DbRawSqlQuery<T> SQLQuery<T>(string sql, params object[] parameters)
        {
            return DbContext.Database.SqlQuery<T>(sql, parameters);
        }
    }
}
