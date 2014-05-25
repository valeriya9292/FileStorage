namespace Interfaces.Repositories
{
    public interface IRepositoryFactory
    {
        IFileRepository FileRepository { get; }
        IUserRepository UserRepository { get; }
    }
}
