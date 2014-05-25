using System;
using System.Collections.Generic;
using Interfaces.Entities;

namespace Interfaces.Repositories
{
    public interface IFileRepository
    {
        IEnumerable<FileEntity> FindAll();
        IEnumerable<FileEntity> FindByOwnerId(Guid ownerId);
        IEnumerable<FileEntity> FindAvailable(Guid userId);
        IEnumerable<FileEntity> FindPublic();

        void Delete(Guid id);
        void DeleteByOwnerId(Guid ownerId);

        void Save(FileEntity file);
    }
}
