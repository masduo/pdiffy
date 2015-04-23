using System;
using Biggy.Core;
using Biggy.Data.Json;
using ImageDiff.Web.Data;

namespace ImageDiff.Web.Infrastructure.Factories
{
	public class BiggyPageListFactory
	{
		public static BiggyList<PageModel> Create()
		{
			return new BiggyList<PageModel>(new JsonStore<PageModel>(AppDomain.CurrentDomain.BaseDirectory, "Data", "Pages"));
		}
	}
}