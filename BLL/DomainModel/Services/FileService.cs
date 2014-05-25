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
        public IEnumerable<FileEntity> FindAvailableFiles(Guid userId)
        {
            return repository.FindAvailable(userId).Select(item => item.ToFileEntity());
        }
        public IEnumerable<FileEntity> FindPublic()
        {
            return repository.FindPublic().Select(item => item.ToFileEntity());
        }

        public void DeleteFile(Guid id)
        {
            repository.Delete(id);
        }
        public void DeleteFileByOwnerId(Guid ownerId)
        {
            repository.DeleteByOwnerId(ownerId);
        }
        public void SaveFile(FileEntity file)
        {
            repository.Save(file.ToDalFile());
            store.Upload(file.Data, file.Path, file.Name);
        }
        
        //void Upload(Stream stream, string path)
        //{
            
        //}
        //Stream Download(string path)
        //{

        //}

        //NOTE: filePath from Web.config
        public FileEntity CreateFileEntity(Stream stream, string fileName, bool isPublic, Guid ownerId, long size, string filePath)
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
