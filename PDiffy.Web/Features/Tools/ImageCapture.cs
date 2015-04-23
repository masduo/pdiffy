using System.Drawing;
using System.IO;
using System.Net;

namespace PDiffy.Web.Features.Tools
{
	public class Capture : ICapture
	{
		public Bitmap GetImageFromUrl(string imageUrl)
		{
			using (var wc = new WebClient())
			using (var ms = new MemoryStream(wc.DownloadData(imageUrl)))
				return new Bitmap(ms);
		}
	}

	public interface ICapture
	{
		Bitmap GetImageFromUrl(string imageUrl);
	}
}