using Library.Domain.Db;
using Library.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Repositories
{
    public class BookTransactionRepository : RepositoryBase<BookTransaction>, IBookTransactionRepository
    {
        public BookTransactionRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }

    public interface IBookTransactionRepository : IRepository<BookTransaction>
    {

    }
}
