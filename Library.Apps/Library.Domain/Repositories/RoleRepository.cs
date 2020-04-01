using Library.Domain.Db;
using Library.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Repositories
{
    public class RoleRepository : RepositoryBase<MasterRole>, IRoleRepository
    {
        public RoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }

    public interface IRoleRepository : IRepository<MasterRole>
    {

    }
}
