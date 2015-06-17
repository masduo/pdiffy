using System.Drawing;
using System.IO;

// ReSharper disable once CheckNamespace
namespace Quarks.ImageExtensions
{
    public static class ImageExtensions
    {
        public static byte[] ToByteArray(this Bitmap image)
        {
            using (image)
            using (var stream = new MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}