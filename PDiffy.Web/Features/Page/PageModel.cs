using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Quarks;

namespace PDiffy.Web.Features.Page
{
    public class PageModel
    {
        [Key]
        public string Name { get; set; }

        public bool ComparisonStillValid { get { return LastComparisonDate != null && LastComparisonDate > SystemTime.Now.AddHours(-72); } }
        public DateTime? LastComparisonDate { get; set; }
        public bool HumanComparisonRequired { get; set; }

        [JsonIgnore]
        public Bitmap OriginalImage
        {
            get
            {
                return string.IsNullOrEmpty(OriginalImageUrl)
                    ? new Bitmap(OriginalImagePath)
                    : Capture(OriginalImageUrl);
            }
        }

        public string OriginalImageUrl { get; set; }
        public string OriginalImagePath { get; set; }

        [JsonIgnore]
        public Bitmap ComparisonImage
        {
            get
            {
                return string.IsNullOrEmpty(ComparisonImageUrl)
                    ? new Bitmap(ComparisonImagePath)
                    : Capture(ComparisonImageUrl);
            }
        }

        public string ComparisonImageUrl { get; set; }
        public string ComparisonImagePath { get; set; }

        public string DifferenceImagePath { get; set; }

        [JsonIgnore]
        public Bitmap DifferenceImage
        {
            get
            {
                return string.IsNullOrWhiteSpace(DifferenceImagePath)
                    ? new ImageDiffTool().CreateDifferenceImage(OriginalImage, ComparisonImage)
                    : new Bitmap(DifferenceImagePath);
            }
        }

        static Bitmap Capture(string imageUrl)
        {
            using (var wc = new WebClient())
            using (var ms = new MemoryStream(wc.DownloadData(imageUrl)))
                return new Bitmap(ms);
        }

        public void GenerateComparison()
        {
            var equal = new ImageDiffTool().Compare(OriginalImage, ComparisonImage);

            if (!equal)
                HumanComparisonRequired = true;
            else
            {
                ComparisonImageUrl = null;
                ComparisonImagePath = null;
            }

            LastComparisonDate = SystemTime.Now;
        }

        public void Approve()
        {
            OriginalImageUrl = ComparisonImageUrl;
            OriginalImagePath = ComparisonImagePath;
            ComparisonImageUrl = null;
            ComparisonImagePath = null;
            DifferenceImagePath = null;
            HumanComparisonRequired = false;
            LastComparisonDate = SystemTime.Now;
        }
    }
}