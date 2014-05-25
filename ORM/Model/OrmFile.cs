using System;

namespace ORM.Model
{
    public class OrmFile
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public bool IsPublic { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime UploadDate { get; set; }

        public virtual OrmUser Owner { get; set; }
    }
}
