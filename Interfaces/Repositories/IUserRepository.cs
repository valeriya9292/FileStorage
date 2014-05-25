using System;
using System.Collections.Generic;
using Interfaces.Entities;

namespace Interfaces.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<DalUser> FindAll();
        void Delete(Guid id);
        void Save(DalUser user);
    }
}
