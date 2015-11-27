using Biggy.Core;
using Biggy.Data.Json;
using PDiffy.Infrastructure;

namespace PDiffy.Data
{
	public class Biggy
	{
		private static BiggyList<Page> _biggyPageList;
	    private static BiggyList<KnownImage> _biggyKnownImageList;

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
                    ?? (_biggyKnownImageList = new BiggyList<KnownImage>(new JsonStore<KnownImage>(Environment.DataStorePath, "Biggy","KnownImages")));
	        }
	    }
	}
}