using System;

namespace Interfaces.Entities
{
    public class DalFile : IDalEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int Size { get; set; }
        public bool IsPublic { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
