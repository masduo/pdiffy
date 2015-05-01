using System;
using System.ComponentModel.DataAnnotations;
using Quarks;

namespace PDiffy.Web.Data
{
	public class PageModel
	{
		[Key]
		public string Name { get; set; }
		public string Url { get; set; }

		public bool ComparisonStillValid { get { return LastComparisonDate != null && LastComparisonDate > SystemTime.Now.AddHours(-72); } }
		public DateTime? LastComparisonDate { get; set; }
		public bool HumanComparisonRequired { get; set; }

		public string OriginalImageUrl { get; set; }
		public string ComparisonImageUrl { get; set; }
		public string DifferenceImagePath { get; set; }

		public int Build { get; set; }
	}
}