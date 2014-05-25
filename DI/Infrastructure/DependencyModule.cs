using DAL.ORM.Repository;
using Interfaces.Repositories;
using Ninject.Modules;

namespace DI.Infrastructure
{
    public class DependencyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IFileRepository>().To<FileRepository>();
            Bind<IFileStore>().To<FileStore>();
        }
    }
}
