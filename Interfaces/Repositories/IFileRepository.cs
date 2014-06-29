using System;
using System.Collections.Generic;
using Interfaces.Entities;

namespace Interfaces.Repositories
{
    public interface IFileRepository
    {
        DalFile FindById(Guid id);
        IEnumerable<DalFile> FindAll();
        IEnumerable<DalFile> FindByOwnerId(Guid ownerId);
        IEnumerable<DalFile> FindPublic();
        IEnumerable<DalFile> FindPublicByName(string fileName);
        IEnumerable<DalFile> FindByNameAndOwnerId(string fileName, Guid ownerId);
        IEnumerable<DalFile> FindByAccurNameAndOwnerId(string fileName, Guid ownerId);


        void Delete(Guid id);
        void DeleteByOwnerId(Guid ownerId);

        void Save(DalFile file);
    }
}
