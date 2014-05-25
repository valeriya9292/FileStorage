using System.Collections.Generic;
using Interfaces.Entities;

namespace Interfaces.Repositories
{
    public interface IRoleRepository
    {
        void Save(IEnumerable<DalRole> roles);
    }
}
