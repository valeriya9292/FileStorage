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
            File.WriteAllBytes(string.Format(@"{0}\{1}", path, fileName), fileBuffer);
        }

        public byte[] Download(string path, string fileName)
        {
            return File.ReadAllBytes(string.Format(@"{0}\{1}", path, fileName));
        }

        public void Delete(string path, string fileName)
        {
            File.Delete(string.Format(@"{0}\{1}", path, fileName));
        }
    }
}
