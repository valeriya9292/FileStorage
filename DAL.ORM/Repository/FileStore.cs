using System.IO;
using Interfaces.Repositories;

namespace DAL.ORM.Repository
{
    public class FileStore : IFileStore
    {
        public void Upload(Stream stream, string path)
        {
            using (var reader = new StreamReader(stream))
            {
                using (var writer = new StreamWriter(File.Open(path, FileMode.Create)))
                {
                    writer.Write(reader.ReadToEnd());
                }
            }           
        }

        public Stream Download(string path)
        {
            using (var reader = new StreamReader(File.OpenRead(path)))
            {
                return reader.BaseStream;
            }
        }
    }
}
