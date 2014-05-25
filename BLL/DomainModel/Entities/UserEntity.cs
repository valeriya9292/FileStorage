using System;

namespace BLL.DomainModel.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
