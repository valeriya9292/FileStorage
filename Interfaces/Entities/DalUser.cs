using System;
namespace Interfaces.Entities
{
    public class DalUser : IDalEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
