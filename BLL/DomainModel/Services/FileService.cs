using System;
using System.Collections.Generic;
using System.IO;
using BLL.Convertions;
using BLL.DomainModel.Entities;
using Interfaces.Repositories;
using System.Linq;

namespace BLL.DomainModel.Services
{
    public class FileService
    {
        private readonly IFileRepository repository;
        private readonly IFileStore store;

        public FileService(IFileRepository repository, IFileStore store)
        {
            this.repository = repository;
            this.store = store;
        }

        public IEnumerable<FileEntity> FindAllFiles()
        {
            return repository.FindAll().Select(item => item.ToFileEntity());
        }
        public IEnumerable<FileEntity> FindFilesByOwnerId(Guid ownerId)
        {
            return repository.FindByOwnerId(ownerId).Select(item => item.ToFileEntity());
        }

        public IEnumerable<FileEntity> FindPublicFiles()
        {
            return repository.FindPublic().Select(item => item.ToFileEntity());
        }

        public IEnumerable<FileEntity> FindPublicFilesByName(string fileName)
        {
            return repository.FindPublic().Where(item => item.Name.Contains(fileName))
                .Select(item => item.ToFileEntity());
        }
        public IEnumerable<FileEntity> FindFilesByNameAndOwnerId(string fileName, Guid ownerId)
        {
            return FindFilesByOwnerId(ownerId).Where(item => item.Name.Contains(fileName));
        }
        public void DeleteFile(Guid id)
        {
            repository.Delete(id);
        }
        public void DeleteFileByOwnerId(Guid ownerId)
        {
            repository.DeleteByOwnerId(ownerId);
        }
        public bool IsFileExisting(string fileName, Guid ownerId)
        {
            return repository.FindByOwnerId(ownerId).Count(item => item.Name.Equals(fileName)) != 0;
        }
        public void SaveFile(FileEntity file)
        {
            repository.Save(file.ToDalFile());
            store.Upload(file.Data, file.Path, file.Name);
        }

        public byte[] Download(string path, string fileName)
        {
            return store.Download(path, fileName);
        }

        //NOTE: filePath from Web.config
        public FileEntity CreateFileEntity(byte[] stream, string fileName, bool isPublic, Guid ownerId, long size, string filePath /*= @"D:\test\out"*/)
        {
            var file = new FileEntity()
                {
                    Id = Guid.NewGuid(),
                    Data = stream,
                    Name = fileName,
                    IsPublic = isPublic,
                    OwnerId = ownerId,
                    Path = filePath,//string.Format(@"{0}\{1}", filePath, User.Email);
                    Size = size,
                    UploadDate = DateTime.Now
                };
            return file;
        }

    }
}
