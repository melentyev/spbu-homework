using System;
using System.IO;
using System.Drawing;

namespace NetTask6.Helpers
{
    internal static class ImagesHelper
    {
        private static Image DummyImage;
        static ImagesHelper()
        {
            DummyImage = Properties.Resources.MovieImagePlaceholder;
        }
        internal static Image FromFile(string path)
        {
            if (String.IsNullOrEmpty(path) || !File.Exists(path))
            {
                return DummyImage;
            }
            try
            { 
                return Image.FromFile(path);
            }
            catch (ArgumentException)
            {
                return DummyImage;
            }
        }
        internal static void DisposeImage(Image img)
        {
            if (!Object.ReferenceEquals(DummyImage, img)) { img.Dispose(); }
        }
    }
}
