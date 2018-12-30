using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace NewCarRental.Helpers
{
    public static class StaticGlobalHelpers
    {
        public static string ImageToString(string path)
        {
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    return Convert.ToBase64String(imageBytes);
                }
            }
        }
    }
}