using System;
using System.Collections.Generic;

namespace ORM.Model
{
    public class OrmUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual OrmRole Role { get; set; }
        public virtual ICollection<OrmFile> Files { get; set; }
    }
}
