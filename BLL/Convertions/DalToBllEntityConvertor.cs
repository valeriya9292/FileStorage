using BLL.DomainModel.Entities;
using Interfaces.Entities;

namespace BLL.Convertions
{
    public static class DalToBllEntityConvertor
    {
        public static FileEntity ToFileEntity(this DalFile dalFile)
        {
            return new FileEntity()
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
        public static DalFile ToDalFile(this FileEntity fileEntity)
        {
            return new DalFile()
            {
                Id = fileEntity.Id,
                IsPublic = fileEntity.IsPublic,
                Name = fileEntity.Name,
                OwnerId = fileEntity.OwnerId,
                Path = fileEntity.Path,
                Size = fileEntity.Size,
                UploadDate = fileEntity.UploadDate
            };
        }

        public static UserEntity ToUserEntity(this DalUser dalUser)
        {
            return new UserEntity()
                {
                    Id = dalUser.Id,
                    Email = dalUser.Email,
                    CreationDate = dalUser.CreationDate,
                    Password = dalUser.Password,
                    RoleId = dalUser.RoleId
                };
        }
        public static DalUser ToDalUser(this UserEntity userEntity)
        {
            return new DalUser()
            {
                Id = userEntity.Id,
                Email = userEntity.Email,
                CreationDate = userEntity.CreationDate,
                Password = userEntity.Password,
                RoleId = userEntity.RoleId
            };
        }
    }
}
