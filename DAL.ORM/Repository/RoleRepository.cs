using System.Collections.Generic;
using Interfaces.Entities;
using Interfaces.Repositories;
using ORM.Model;

namespace DAL.ORM.Repository
{
    public class RoleRepository: IRoleRepository
    {
        private readonly FileStorageDbContext context = new FileStorageDbContext();

        public void Save(IEnumerable<DalRole> roles)
        {
            using (context)
            {
                foreach (var dalRole in roles)
                {
                    context.Roles.Add(new OrmRole() {Id = dalRole.Id, Name = dalRole.Name});
                }
                context.SaveChanges();
            }
        }
    }
}
