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
                //adding milliseconds to avoid filename conflict
                var fullPath = Path.Combine(Environment.ImageStorePath, string.Join(".", name, SystemTime.Now.ToString("yyyyMMdd-HHmmss.ffff"), "png"));
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
			var files = Directory.GetFiles(Environment.ImageStorePath)
				.Where(path =>
					Path.GetFileName(path).Name() == name &&
					Path.GetFileName(path).Type() == imageType).ToArray();

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