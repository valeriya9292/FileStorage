namespace Interfaces.Repositories
{
    public interface IFileStore
    {
        void Upload(byte[]fileBuffer, string path, string fileName);
        byte[] Download(string path, string fileName);
    }
}
