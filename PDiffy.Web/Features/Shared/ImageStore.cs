using System;
using System.IO;
using System.Linq;
using Quarks;
using Environment = PDiffy.Web.Infrastructure.Environment;

namespace PDiffy.Web.Features.Shared
{
    public class ImageStore : IImageStore
    {
        public string SaveImage(System.Drawing.Image image, string name)
        {
            var fullPath = Path.Combine(Environment.ImageStorePath, string.Join(".", name, SystemTime.Now.ToString("yyyyMMdd-HHmmss"), "png"));
            var folder = Path.GetDirectoryName(fullPath);

            if (folder != null && !Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            image.Save(fullPath);

            return fullPath;
        }

        public void DeleteImage(string name)
        {
            throw new NotImplementedException();
        }

        public void DeleteImages()
        {
            //Directory.Delete(Environment.ImageStorePath, true);
            throw new NotImplementedException();
        }

        public string[] GetImages(string name, string imageType)
        {
            var fullPath = Path.Combine(Environment.ImageStorePath, string.Join(".", name, SystemTime.Now.ToString("yyyyMMdd-HHmmss"), "png"));
            var folder = Path.GetDirectoryName(fullPath);

            var files = folder == Environment.ImageStorePath
                ? Directory.GetFiles(folder)
                    .Where(path => Path.GetFileName(path).Split('.')[0] == name && Path.GetFileName(path).Split('.')[1] == imageType).ToArray()
                : Directory.GetFiles(folder).Where(path => Path.GetFileName(path).Split('.')[1] == imageType).ToArray();

            return files;

        }
    }

    public interface IImageStore
    {
        string SaveImage(System.Drawing.Image image, string name);
        string[] GetImages(string name, string imageType);
        void DeleteImages();
        void DeleteImage(string name);
    }
}