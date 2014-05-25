using System.Collections.Generic;
using Interfaces.Entities;

namespace Interfaces.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<UserEntity> FindAll();
        bool Delete(int id);
        bool Save(UserEntity user);
    }
}
