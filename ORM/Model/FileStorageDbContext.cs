using System.Data.Entity;

namespace ORM
{
    class FileStorageDbContext : DbContext
    {
        public FileStorageDbContext()
            : base("FileStorageDb")
        {
        }

        public DbSet<File> Files { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
