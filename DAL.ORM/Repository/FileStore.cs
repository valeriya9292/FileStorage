using System.IO;
using Interfaces.Repositories;

namespace DAL.ORM.Repository
{
    public class FileStore : IFileStore
    {
        public void Upload(Stream stream, string path, string fileName)
        {
            using (var reader = new StreamReader(stream))
            {
                using (var writer = new StreamWriter(
                    File.Open(string.Format(@"{0}\{1}",path, fileName), FileMode.Create)))
                {
                    writer.Write(reader.ReadToEnd());
                }
            }           
        }

        public Stream Download(string path, string fileName)
        {
            using (var reader = new StreamReader(File.OpenRead((string.Format(@"{0}\{1}",path, fileName)))))
            {
                return reader.BaseStream;
            }
        }
    }
}
