using System;
using System.Collections.Generic;
using Interfaces.Entities;

namespace Interfaces.Repositories
{
    public interface IFileRepository
    {
        IEnumerable<DalFile> FindAll();
        IEnumerable<DalFile> FindByOwnerId(Guid ownerId);
        IEnumerable<DalFile> FindAvailable(Guid userId);
        IEnumerable<DalFile> FindPublic();

        void Delete(Guid id);
        void DeleteByOwnerId(Guid ownerId);

        void Save(DalFile file);
    }
}
