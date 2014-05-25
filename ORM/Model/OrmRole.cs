using System.Collections.Generic;

namespace ORM.Model
{
    public class OrmRole
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //NOTE: is it neccessary to have this prop ?
        public virtual ICollection<OrmUser> Users { get; set; }
    }
}
