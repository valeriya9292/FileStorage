using System;
using System.Collections.Generic;
using Interfaces.Entities;

namespace Interfaces.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<DalUser> FindAll();
        IEnumerable<DalUser> FindByRole(string roleName);
        void Delete(Guid id);
        void Save(DalUser user);
    }
}
