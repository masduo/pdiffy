using System;
using Biggy.Core;
using Biggy.Data.Json;
using Environment = PDiffy.Infrastructure.Environment;

namespace PDiffy.Data
{
	public class Biggy
	{
		//NOTE: If it is needed to clean data, leave an empty json array [{}] in the file or delete it,
		// otherwise biggy's list initialization will fail with source cannot be null exception.

		private static BiggyList<Page> _biggyPageList;
		private static BiggyList<KnownImage> _biggyKnownImageList;
		private static BiggyList<TextDiffPage> _textDiffPageList;

		public static BiggyList<Page> PageList
		{
			get
			{
				return _biggyPageList
					?? (_biggyPageList = new BiggyList<Page>(new JsonStore<Page>(Environment.DataStorePath, "Biggy", "Pages")));
			}
		}

		public static BiggyList<KnownImage> KnownImageList
		{
			get
			{
				return _biggyKnownImageList
					?? (_biggyKnownImageList = new BiggyList<KnownImage>(new JsonStore<KnownImage>(Environment.DataStorePath, "Biggy", "KnownImages")));
			}
		}

		public static BiggyList<TextDiffPage> TextDiffPageList
		{
			get
			{
				return _textDiffPageList
						?? (_textDiffPageList = new BiggyList<TextDiffPage>(new JsonStore<TextDiffPage>(Environment.DataStorePath, "Biggy", "TextDiffPages")));
			}
		}
	}
}