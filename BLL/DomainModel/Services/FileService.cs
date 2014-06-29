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
            return repository.FindPublicByName(fileName).Select(item => item.ToFileEntity());
        }

        public IEnumerable<FileEntity> FindFilesByNameAndOwnerId(string fileName, Guid ownerId)
        {
            return repository.FindByNameAndOwnerId(fileName, ownerId).Select(item => item.ToFileEntity());
        }


        public void DeleteFile(Guid id)
        {
            var file = repository.FindById(id);
            store.Delete(file.Path, file.Name);
            repository.Delete(id);
        }
        public void DeleteFileByOwnerId(Guid ownerId)
        {
            repository.DeleteByOwnerId(ownerId);
        }

        public bool IsFileExisting(string fileName, Guid ownerId)
        {
            return repository.FindByAccurNameAndOwnerId(fileName, ownerId).Count() != 0;
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

        public FileEntity CreateFileEntity(byte[] stream, string fileName, bool isPublic, Guid ownerId, long size, string filePath)
        {
            var file = new FileEntity()
                {
                   // Id = Guid.NewGuid(),
                    Data = stream,
                    Name = fileName,
                    IsPublic = isPublic,
                    OwnerId = ownerId,
                    Path = filePath,
                    Size = size,
                    UploadDate = DateTime.Now
                };
            return file;
        }
    }
}
