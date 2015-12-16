using System;
using System.ComponentModel.DataAnnotations;
using Quarks;

namespace PDiffy.Data
{
	public class TextDiffPage
	{
		private string _originalText = string.Empty;
		private string _comparisonText = string.Empty;
		private string _differenceText = string.Empty;

		[Key]
		public string Name { get; set; }

		public bool ComparisonStillValid { get { return LastComparisonDate != null && LastComparisonDate > SystemTime.Now.AddHours(-72); } }
		public DateTime? LastComparisonDate { get; set; }
		public bool HumanComparisonRequired { get; set; }

		public string OriginalText
		{
			get { return _originalText; }
			set { _originalText = value ?? string.Empty; }
		}

		public string ComparisonText
		{
			get { return _comparisonText; }
			set { _comparisonText = value ?? string.Empty; }
		}

		//public IList<Diff> DifferenceText { get; set; }
		public string DifferenceText
		{
			get { return _differenceText; }
			set { _differenceText = value ?? string.Empty; }
		}
	}
}