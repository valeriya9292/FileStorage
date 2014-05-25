using System;
namespace Interfaces.Entities
{
    public class UserEntity : IEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
