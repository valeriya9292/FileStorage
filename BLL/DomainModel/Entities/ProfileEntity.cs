using System;

namespace BLL.DomainModel.Entities
{
    public class ProfileEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid UserId { get; set; }
    }
}
