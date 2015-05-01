using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using PDiffy.Web.Data;
using PDiffy.Web.Features.Tools;
using Quarks;

namespace PDiffy.Web.Features.Shared
{
	public class ImageGenerator : IImageGenerator
	{
		readonly IImageDiffTool _imageDiffTool;
		readonly ICapture _capture;
		private readonly IImageStore _imageStore;

		public ImageGenerator(IImageDiffTool imageDiffTool, ICapture capture, IImageStore imageStore)
		{
			_imageDiffTool = imageDiffTool;
			_capture = capture;
			_imageStore = imageStore;
		}
		public byte[] GenerateDifference(PageModel pageModel)
		{
			Bitmap differenceImage;

			if (string.IsNullOrWhiteSpace(pageModel.DifferenceImagePath))
			{
				var originalImage = _capture.GetImageFromUrl(pageModel.OriginalImageUrl);
				var comparisonImage = _capture.GetImageFromUrl(pageModel.ComparisonImageUrl);
				differenceImage = _imageDiffTool.CreateDifferenceImage(originalImage, comparisonImage);
				pageModel.DifferenceImagePath = _imageStore.SaveImage(differenceImage, pageModel.Name);
			}

			differenceImage = new Bitmap(pageModel.DifferenceImagePath);

			return (byte[])new ImageConverter().ConvertTo(differenceImage, typeof(byte[]));
		}

		public IEnumerable<Image> GenerateDifferences(string name)
		{
			return _imageStore.GetImages(name).Select(x => new Image
			{
				CreatedDate =
					DateTime.ParseExact(Path.GetFileName(x).Split('.')[1], "yyyyMMdd-HHmmss", CultureInfo.InvariantCulture),
				ImageString = "data:image/png;base64," + Convert.ToBase64String((byte[])new ImageConverter().ConvertTo(new Bitmap(x), typeof(byte[])))
			});
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
		IEnumerable<Image> GenerateDifferences(string name);
	}
}