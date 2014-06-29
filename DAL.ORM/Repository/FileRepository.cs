using System;
using System.Collections.Generic;
using System.Linq;
using DAL.ORM.Convertions;
using Interfaces.Entities;
using Interfaces.Repositories;
using ORM.Model;

namespace DAL.ORM.Repository
{
    public class FileRepository : IFileRepository
    {
        public DalFile FindById(Guid id)
        {
            using (var context = new FileStorageDbContext())
            {
                return context.Files.FirstOrDefault(f => f.Id == id).ToDalFile();
            }
        }

        public IEnumerable<DalFile> FindAll()
        {
            using (var context = new FileStorageDbContext())
            {
                return context.Files.AsEnumerable().Select(elem => elem.ToDalFile()).ToList();
            }
        }

        public IEnumerable<DalFile> FindByOwnerId(Guid ownerId)
        {
            using (var context = new FileStorageDbContext())
            {
                return context.Files.AsEnumerable()
                    .Where(elem => elem.ToDalFile().OwnerId == ownerId)
                    .Select(elem => elem.ToDalFile()).ToList();
            }
        }

        public IEnumerable<DalFile> FindPublic()
        {
            using (var context = new FileStorageDbContext())
            {
                return context.Files.AsEnumerable()
                    .Where(elem => elem.ToDalFile().IsPublic)
                    .Select(elem => elem.ToDalFile()).ToList();
            }
        }

        public IEnumerable<DalFile> FindPublicByName(string fileName)
        {
            using (var context = new FileStorageDbContext())
            {
                return context.Files.AsEnumerable()
                              .Where(elem => elem.ToDalFile().IsPublic && elem.Name.Contains(fileName))
                              .Select(elem => elem.ToDalFile()).ToList();

            }
        }

        public IEnumerable<DalFile> FindByNameAndOwnerId(string fileName, Guid ownerId)
        {
            using (var context = new FileStorageDbContext())
            {
                return context.Files.AsEnumerable()
                              .Where(item => item.Name.Contains(fileName) && item.OwnerId == ownerId)
                              .Select(item => item.ToDalFile()).ToList();

            }
        }

        public IEnumerable<DalFile> FindByAccurNameAndOwnerId(string fileName, Guid ownerId)
        {
            using (var context = new FileStorageDbContext())
            {
                return context.Files.AsEnumerable()
                              .Where(item => item.Name.Equals(fileName) && item.OwnerId == ownerId)
                              .Select(item => item.ToDalFile()).ToList();

            }
        }

        public void Delete(Guid id)
        {
            using (var context = new FileStorageDbContext())
            {
                var file = context.Files.FirstOrDefault(f => f.Id == id);
                if (file == null) return;
                context.Files.Remove(file);
                context.SaveChanges();
            }
        }

        public void DeleteByOwnerId(Guid ownerId)
        {
            using (var context = new FileStorageDbContext())
            {
                var files = context.Files
                    .Where(elem => elem.OwnerId == ownerId)
                    .Select(elem => elem);
                foreach (var file in files.Where(file => file != null))
                {
                    context.Files.Remove(file);
                    context.SaveChanges();
                }
            }
        }

        public void Save(DalFile file)
        {
            using (var context = new FileStorageDbContext())
            {
                var oldFile = context.Files.Where(f => f.Name.Equals(file.Name) && f.Path.Equals(file.Path)).Select(f => f);
                file.Id = oldFile.Count() != 0 ? oldFile.First().Id : Guid.NewGuid();
                context.Files.RemoveRange(oldFile);
                context.Files.Add(file.ToOrmFile());
                context.SaveChanges();
            }
        }
    }
}
