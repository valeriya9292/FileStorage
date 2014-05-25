using System;
using System.Collections.Generic;
using BLL.Convertions;
using BLL.DomainModel.Entities;
using Interfaces.Repositories;
using System.Linq;

namespace BLL.DomainModel.Services
{
    public class UserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }
        
        public IEnumerable<UserEntity> FindAllUsers()
        {
            return repository.FindAll().Select(item => item.ToUserEntity()).ToList();
        }
        public void DeleteUser(Guid id)
        {
            repository.Delete(id);
        }
        public void SaveUser(UserEntity user)
        {
            repository.Save(user.ToDalUser());
        }
    }
}
