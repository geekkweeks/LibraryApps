using Library.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.IServices.Role
{
    public interface IRoleService
    {
        IQueryable<RoleServiceModel> GetAllRole();
    }
}
