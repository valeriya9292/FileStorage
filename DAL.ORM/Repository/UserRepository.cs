using System;
using System.Collections.Generic;
using System.Linq;
using DAL.ORM.Convertions;
using Interfaces.Entities;
using Interfaces.Repositories;
using ORM.Model;

namespace DAL.ORM.Repository
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<DalUser> FindAll()
        {
            IEnumerable<DalUser> users;
            using (var context = new FileStorageDbContext())
            {
                users = context.Users.AsEnumerable().Select(elem => elem.ToDalUser()).ToList();
            }
            return users;
        }

        public IEnumerable<DalUser> FindByRole(string roleName)
        {
            IEnumerable<DalUser> users;
            using (var context = new FileStorageDbContext())
            {
                users = context.Users.AsEnumerable()
                    .Where(elem => elem.Role.Name.Equals(roleName))
                    .Select(elem => elem.ToDalUser()).ToList();
            }
            return users;
        }

        public void Delete(Guid id)
        {
            using (var context = new FileStorageDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Id == id);
                if (user == null) return;
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }

        public void Save(DalUser user)
        {
            using (var context = new FileStorageDbContext())
            {
                context.Users.Add(user.ToOrmUser());
                context.SaveChanges();
            } 
        }
    }
}
