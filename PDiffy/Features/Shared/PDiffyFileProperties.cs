using System;
using System.Globalization;

namespace PDiffy.Features.Shared
{

    public static class PDiffyFileProperties
    {
        public static string Name(this string fileName)
        {
            return fileName.Split('.')[0];
        }

        public static string Type(this string fileName)
        {
            return fileName.Split('.')[1];
        }

        public static DateTime Date(this string fileName)
        {
            return DateTime.ParseExact(fileName.Split('.')[2], "yyyyMMdd-HHmmss", CultureInfo.InvariantCulture);
        }
    }
}