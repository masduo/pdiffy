using System;
using System.Drawing;
using System.IO;
using PDiffy.Web.Features.Shared;
using PDiffy.Web.Quarks.ImageExtensions;

namespace PDiffy.Web.Features.History
{
    public class HistoricalImage
    {
        public HistoricalImage(string imagePath)
        {
            ImagePath = imagePath;
        }

        public string ImagePath { get; set; }

        public string ImageString
        {
            get
            {
                return "data:image/png;base64," + Convert.ToBase64String(new Bitmap(ImagePath).ToByteArray());
            }
        }

        public DateTime? CreatedDate
        {
            get
            {
                var fileName = Path.GetFileName(ImagePath);
                if (fileName != null)
                    return fileName.Date();
                return null;
            }
        }
    }
}