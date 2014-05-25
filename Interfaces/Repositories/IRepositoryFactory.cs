namespace Interfaces.Repositories
{
    public interface IRepositoryFactory
    {
        IFileRepository CreateFileRepository();
        IUserRepository CreateUserRepository();
    }
}
