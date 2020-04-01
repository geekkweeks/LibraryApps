using Library.Domain.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private LibraryDBContext dbContext;
        public LibraryDBContext Init()
        {
            return dbContext ?? (dbContext = new LibraryDBContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}
