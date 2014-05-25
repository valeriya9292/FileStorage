using System.IO;

namespace Interfaces.Repositories
{
    public interface IFileStore
    {
        void Upload(Stream stream, string path);
        Stream Download(string path);
    }
}
