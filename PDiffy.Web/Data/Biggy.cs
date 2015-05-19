using Biggy.Core;
using Biggy.Data.Json;
using PDiffy.Web.Infrastructure;

namespace PDiffy.Web.Data
{
	public class Biggy
	{
		private static BiggyList<PageModel> _biggyPageList;

		public static BiggyList<PageModel> PageList
		{
			get
			{
				return _biggyPageList 
					?? (_biggyPageList = new BiggyList<PageModel>(new JsonStore<PageModel>(Environment.DataStorePath, "Biggy", "Pages")));
			}
		}
	}
}