using Biggy.Core;
using Biggy.Data.Json;
using Environment = PDiffy.Infrastructure.Environment;

namespace PDiffy.Data
{
	public class Biggy
	{
		//NOTE: If it is needed to clean data, leave an empty json array [{}] in the file or delete it,
		// otherwise biggy's list initialization will fail with source cannot be null exception.

		private static BiggyList<ImageComparison> _imageComparisons;
		private static BiggyList<TextComparison> _textComparisons;
		private static BiggyList<KnownImage> _knownImages;

		public static BiggyList<ImageComparison> ImageComparisons
		{
			get { return _imageComparisons ?? (_imageComparisons = new BiggyList<ImageComparison>(new JsonStore<ImageComparison>(Environment.DataStorePath, "Biggy", "ImageComparisons"))); }
		}

		public static BiggyList<TextComparison> TextComparisons
		{
			get { return _textComparisons ?? (_textComparisons = new BiggyList<TextComparison>(new JsonStore<TextComparison>(Environment.DataStorePath, "Biggy", "TextComparisons"))); }
		}

		public static BiggyList<KnownImage> KnownImageList
		{
			get { return _knownImages ?? (_knownImages = new BiggyList<KnownImage>(new JsonStore<KnownImage>(Environment.DataStorePath, "Biggy", "KnownImages"))); }
		}
	}
}