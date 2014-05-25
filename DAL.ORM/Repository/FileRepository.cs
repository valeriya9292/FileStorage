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
        private readonly FileStorageDbContext context = new FileStorageDbContext();

        public IEnumerable<DalFile> FindAll()
        {
            using (context)
            {
                return context.Files.AsEnumerable().Select(elem => elem.ToDalFile()).ToList();
            }
        }

        public IEnumerable<DalFile> FindByOwnerId(Guid ownerId)
        {
            using (context)
            {
                return context.Files.AsEnumerable()
                    .Where(elem => elem.ToDalFile().OwnerId == ownerId)
                    .Select(elem => elem.ToDalFile()).ToList();
            }
        }

        public IEnumerable<DalFile> FindAvailable(Guid userId)
        {
            using (context)
            {
                return context.Files.AsEnumerable()
                    .Where(elem => elem.ToDalFile().OwnerId == userId || elem.ToDalFile().IsPublic)
                    .Select(elem => elem.ToDalFile()).ToList();
            }
        }

        public IEnumerable<DalFile> FindPublic()
        {
            using (context)
            {
                return context.Files.AsEnumerable()
                    .Where(elem => elem.ToDalFile().IsPublic)
                    .Select(elem => elem.ToDalFile()).ToList();
            }
        }

        public void Delete(Guid id)
        {
            using (context)
            {
                var file = context.Files.FirstOrDefault(f => f.Id == id);
                if (file == null) return;
                context.Files.Remove(file);
                context.SaveChanges();
            }
        }

        public void DeleteByOwnerId(Guid ownerId)
        {
            using (context)
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
            using (context)
            {
                context.Files.Add(file.ToOrmFile());
                context.SaveChanges();
            }
        }
    }
}
