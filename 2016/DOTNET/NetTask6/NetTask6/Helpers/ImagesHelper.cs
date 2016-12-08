using System;
using System.Drawing;

namespace NetTask6.Helpers
{
    static class ImagesHelper
    {
        internal static Image FromFile(string path)
        {
            if (String.IsNullOrEmpty(path))
            {
                return null;
            }
            return Image.FromFile(path);
        }
    }
}
