using System.Collections.Generic;
using Interfaces.Entities;

namespace Interfaces.Repositories
{
    public interface IRoleRepository
    {
        IEnumerable<DalRole> FindAll();
        void Save(IEnumerable<DalRole> roles);
    }
}
