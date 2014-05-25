using Interfaces.Repositories;

namespace DAL.ORM.Repository
{
    public class RepositoryFactory: IRepositoryFactory
    {
        public IFileRepository CreateFileRepository()
        {
            return new FileRepository();
        }

        public IUserRepository CreateUserRepository()
        {
            return new UserRepository();
        }
    }
}
