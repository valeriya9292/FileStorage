using System;
using System.Collections.Generic;
using System.IO;
using BLL.DomainModel.Entities;
using BLL.DomainModel.Services;
using DAL.ORM.Repository;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var pathOnClient = @"D:\test\in";
            var pathOnServer = @"D:\test\out";
            var fileName = "test.txt";

            var roles = new List<RoleEntity>()
                {
                    new RoleEntity() { Id = 1, Name = "Admin"},
                    new RoleEntity() { Id = 2, Name = "User"},
                    new RoleEntity() { Id = 3, Name = "Anonym"}
                };
            //NOTE: DAL shouldn't be in refrences
            //var roleService = new RoleService(new RoleRepository());
            //roleService.SaveRoles(roles);

            var user = new UserEntity()
                {
                    Id =  new Guid(),
                    CreationDate = DateTime.Now,
                    Email = "testUser@emal.com",
                    Password = "123",
                    RoleId = 1
                };

            //var userService = new UserService(new UserRepository());
            //userService.SaveUser(user);

            var fileService = new FileService(new FileRepository(), new FileStore());

            var fileStream = File.ReadAllBytes(string.Format(@"{0}\{1}", pathOnClient, fileName));

            var file = fileService.CreateFileEntity(
                fileStream, fileName, true,new Guid(), fileStream.Length, pathOnServer);

            fileService.SaveFile(file);

        }
    }
}
