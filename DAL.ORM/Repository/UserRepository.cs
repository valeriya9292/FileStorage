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
        private readonly FileStorageDbContext context = new FileStorageDbContext();

        public IEnumerable<DalUser> FindAll()
        {
            using (context)
            {
                return context.Users.AsEnumerable().Select(elem => elem.ToDalUser()).ToList();
            }
        }

        public void Delete(Guid id)
        {
            using (context)
            {
                var user = context.Users.FirstOrDefault(u => u.Id == id);
                if (user == null) return;
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }

        public void Save(DalUser user)
        {
            using (context)
            {
                context.Users.Add(user.ToOrmUser());
                context.SaveChanges();
            } 
        }
    }
}
