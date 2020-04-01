using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Infrastructure
{
    public interface IUnitOfWork
    {
        DbRawSqlQuery<T> SQLQuery<T>(string sql, params object[] parameters);
        void Commit();
    }
}
