using System.Collections.Generic;
using System.Linq;
using BLL.DomainModel.Entities;
using Interfaces.Entities;
using Interfaces.Repositories;

namespace BLL.DomainModel.Services
{
    public class RoleService
    {
        private readonly IRoleRepository repository;

        public RoleService(IRoleRepository repository)
        {
            this.repository = repository;
        }

        public void SaveRoles(IEnumerable<RoleEntity> roles)
        {
            repository.Save(roles.Select(item => new DalRole(){Id = item.Id, Name = item.Name}));
        }
    }
}
