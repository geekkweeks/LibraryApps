using Library.Domain.Db;
using Library.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Repositories
{
    public class BookRepository : RepositoryBase<MasterBook>, IBookRepository
    {
        public BookRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }

    public interface IBookRepository : IRepository<MasterBook>
    {

    }
}
