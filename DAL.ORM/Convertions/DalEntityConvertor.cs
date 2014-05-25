using Interfaces.Entities;
using ORM.Model;

namespace DAL.ORM.Convertions
{
    public static class DalEntityConvertor
    {
        public static OrmFile ToOrmFile(this DalFile dalFile)
        {
            return new OrmFile()
            {
                Id = dalFile.Id,
                IsPublic = dalFile.IsPublic,
                Name = dalFile.Name,
                OwnerId = dalFile.OwnerId,
                Path = dalFile.Path,
                Size = dalFile.Size,
                UploadDate = dalFile.UploadDate
            };
        }

        public static DalFile ToDalFile(this OrmFile ormFile)
        {
            return new DalFile()
            {
                Id = ormFile.Id,
                IsPublic = ormFile.IsPublic,
                Name = ormFile.Name,
                OwnerId = ormFile.OwnerId,
                Path = ormFile.Path,
                Size = ormFile.Size,
                UploadDate = ormFile.UploadDate
            };
        }

        public static OrmUser ToOrmUser(this DalUser dalUser)
        {
            return new OrmUser()
                {
                    Id = dalUser.Id,
                    Email = dalUser.Email,
                    CreationDate = dalUser.CreationDate,
                    Password = dalUser.Password,
                    RoleId = dalUser.RoleId
                };
        }

        public static DalUser ToDalUser(this OrmUser ormUser)
        {
            return new DalUser()
                {
                    Id = ormUser.Id,
                    Email = ormUser.Email,
                    CreationDate = ormUser.CreationDate,
                    Password = ormUser.Password,
                    RoleId = ormUser.RoleId
                };
        }
    }
}

