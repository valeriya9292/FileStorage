using System.Data.Entity;

namespace ORM.Model
{
    public class FileStorageDbContext : DbContext
    {
        public FileStorageDbContext()
            : base("FileStorageDb")
        {
        }

        public DbSet<OrmFile> Files { get; set; }
        public DbSet<OrmUser> Users { get; set; }
        public DbSet<OrmRole> Roles { get; set; }
    }
}
