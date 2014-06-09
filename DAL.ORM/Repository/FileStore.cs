using System.IO;
using Interfaces.Repositories;

namespace DAL.ORM.Repository
{
    public class FileStore : IFileStore
    {
        public void Upload(byte[] fileBuffer, string path, string fileName)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            using (var writer = new StreamWriter(
                File.Open(string.Format(@"{0}\{1}", path, fileName), FileMode.Create)))
            {
                writer.Write(fileBuffer);
            }
        }

        public byte[] Download(string path, string fileName)
        {
            return File.ReadAllBytes(string.Format(@"{0}\{1}", path, fileName));
        }
    }
}
