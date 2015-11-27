using System.IO;
using System.Linq;
using PDiffy.Infrastructure;
using Quarks;
using Quarks.IEnumerableExtensions;

namespace PDiffy.Data.Stores
{
    public class ImageStore : IImageStore
    {
        public string Save(System.Drawing.Image image, string name)
        {
            using (image)
            {

                var fullPath = Path.Combine(Environment.ImageStorePath, string.Join(".", name, SystemTime.Now.ToString("yyyyMMdd-HHmmss"), "png"));
                var folder = Path.GetDirectoryName(fullPath);

                if (folder != null && !Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                image.Save(fullPath);

                return fullPath;
            }
        }

        public void Delete(string name, string[] imageTypes)
        {
            imageTypes.SelectMany(imageType => Get(name, imageType)).ForEach(File.Delete);
        }

        public void DeleteAll()
        {
            Directory.Delete(Environment.ImageStorePath, true);
        }

        public string[] Get(string name, string imageType)
        {
            var fullPath = Path.Combine(Environment.ImageStorePath, string.Join(".", name, SystemTime.Now.ToString("yyyyMMdd-HHmmss"), "png"));
            var folder = Path.GetDirectoryName(fullPath);

            var files = folder == Environment.ImageStorePath
                ? Directory.GetFiles(folder)
                    .Where(path => Path.GetFileName(path).Name() == name && Path.GetFileName(path).Type() == imageType).ToArray()
                : Directory.GetFiles(folder).Where(path => Path.GetFileName(path).Type() == imageType).ToArray();

            return files;
        }
    }

    public interface IImageStore
    {
        string Save(System.Drawing.Image image, string name);
        string[] Get(string name, string imageType);
        void DeleteAll();
        void Delete(string name, string[] imageTypes);
    }
}