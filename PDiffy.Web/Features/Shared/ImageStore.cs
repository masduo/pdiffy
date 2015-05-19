using System.IO;
using System.Linq;
using PDiffy.Web.Infrastructure;
using Quarks;

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

		public string[] GetImages(string name)
		{
			var fullPath = Path.Combine(Environment.ImageStorePath, string.Join(".", name, SystemTime.Now.ToString("yyyyMMdd-HHmmss"), "png"));
			var folder = Path.GetDirectoryName(fullPath);

			var files = folder == Environment.ImageStorePath
				? Directory.GetFiles(folder)
					.Where(path => Path.GetFileName(path).Split('.')[0] == name).ToArray()
				: Directory.GetFiles(folder);

			return files;

		}
	}

	public interface IImageStore
	{
		string SaveImage(System.Drawing.Image image, string name);
		string[] GetImages(string name);
	}
}