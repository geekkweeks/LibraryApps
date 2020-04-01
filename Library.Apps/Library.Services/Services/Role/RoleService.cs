using Library.Domain.Repositories;
using Library.Services.IServices.Role;
using Library.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services.Role
{
    public class RoleService : IRoleService
    {
        private IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            this._roleRepository = roleRepository;
        }

        public IQueryable<RoleServiceModel> GetAllRole()
        {
            IQueryable<RoleServiceModel> list = _roleRepository.GetAll().Select(s => new RoleServiceModel()
            {
                RoleId = s.RoleId,
                RoleName = s.RoleName
            });

            return list;
        }
    }
}
