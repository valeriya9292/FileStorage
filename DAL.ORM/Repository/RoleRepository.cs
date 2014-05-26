using System.Collections.Generic;
using System.Linq;
using DAL.ORM.Convertions;
using Interfaces.Entities;
using Interfaces.Repositories;
using ORM.Model;

namespace DAL.ORM.Repository
{
    public class RoleRepository: IRoleRepository
    {

        public IEnumerable<DalRole> FindAll()
        {
            using (var context = new FileStorageDbContext())
            {
                return context.Roles.AsEnumerable().Select(elem => elem.ToDalRole()).ToList(); ;
            }
        }

        public void Save(IEnumerable<DalRole> roles)
        {
            using (var context = new FileStorageDbContext())
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
