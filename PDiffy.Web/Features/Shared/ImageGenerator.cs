using System.Drawing;
using ImageDiff;
using PDiffy.Web.Data;
using PDiffy.Web.Features.Tools;
using Quarks;

namespace PDiffy.Web.Features.Shared
{
	public class ImageGenerator : IImageGenerator
	{
		readonly IImageDiffTool _imageDiffTool;
		readonly ICapture _capture;

		public ImageGenerator(IImageDiffTool imageDiffTool, ICapture capture)
		{
			_imageDiffTool = imageDiffTool;
			_capture = capture;
		}
		public byte[] GenerateDifference(PageModel pageModel)
		{
			var originalImage = _capture.GetImageFromUrl(pageModel.OriginalImageUrl);
			var comparisonImage = _capture.GetImageFromUrl(pageModel.ComparisonImageUrl);

			return (byte[])new ImageConverter().ConvertTo(_imageDiffTool.CreateDifferenceImage(originalImage, comparisonImage), typeof(byte[]));
		}

		public void GenerateComparison(PageModel pageModel)
		{
			var originalImage = _capture.GetImageFromUrl(pageModel.OriginalImageUrl);
			var comparisonImage = _capture.GetImageFromUrl(pageModel.ComparisonImageUrl);

			var equal = _imageDiffTool.Compare(originalImage, comparisonImage);

			if (!equal)
				pageModel.HumanComparisonRequired = true;

			pageModel.LastComparisonDate = SystemTime.Now;
		}
	}

	public interface IImageGenerator
	{
		void GenerateComparison(PageModel pageModel);
		byte[] GenerateDifference(PageModel page);
	}
}