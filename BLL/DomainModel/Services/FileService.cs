using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
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

        IEnumerable<FileEntity> FindAllFiles()
        {
            return repository.FindAll().Select(item => item.ToFileEntity());
        }
        IEnumerable<FileEntity> FindFilesByOwnerId(Guid ownerId)
        {
            return repository.FindByOwnerId(ownerId).Select(item => item.ToFileEntity());
        }
        IEnumerable<FileEntity> FindAvailableFiles(Guid userId)
        {
            return repository.FindAvailable(userId).Select(item => item.ToFileEntity());
        }
        IEnumerable<FileEntity> FindPublic()
        {
            return repository.FindPublic().Select(item => item.ToFileEntity());
        }

        void DeleteFile(Guid id)
        {
            repository.Delete(id);
        }

        void DeleteFileByOwnerId(Guid ownerId)
        {
            repository.DeleteByOwnerId(ownerId);
        }

        void SaveFile(FileEntity file)
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
        private FileEntity CreateFileEntity(Stream stream, string fileName, bool isPublic, Guid ownerId, int size, string filePath)
        {
            var file = new FileEntity()
                {
                    Id = Guid.NewGuid(),
                    Data = stream,
                    Name = fileName,
                    IsPublic = isPublic,
                    OwnerId = ownerId,
                    Path = null,//string.Format(@"{0}\{1}", filePath, User.Email);
                    Size = size,
                    UploadDate = DateTime.Now
                };
            return file;
        }

    }
}
