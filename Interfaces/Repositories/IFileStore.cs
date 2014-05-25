using System.IO;

namespace Interfaces.Repositories
{
    public interface IFileStore
    {
        void Upload(Stream stream, string path, string fileName);
        Stream Download(string path, string fileName);
    }
}
