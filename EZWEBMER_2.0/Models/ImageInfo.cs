using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace EZWEBMER_2._0.Models
{
    class ImageInfo : MediaInfo
    {
        public bool isValid;
        public String Path { get; set; }
        private BitmapImage image;
        public GifHandler gif;
        public int Height;
        public int Width;

        public ImageInfo(String path) {
            isValid = false;
            Load(path);
            isValid = true;
        }

        public void Load(string path)
        {
            this.Path = path;
            image = new BitmapImage(new Uri(path));
            if (GifHandler.isGif(path)) gif = new GifHandler(path);
            Height = image.PixelHeight;
            Width = image.PixelWidth;

            if (Height % 2 != 0)
            {
                System.Windows.MessageBox.Show("Wrong Height. Must be even");
                //AddPixel(true);
                return;
            }
            if (Width % 2 != 0)
            {
                System.Windows.MessageBox.Show("Wrong Width. Must be even");
                //AddPixel(false);
                return;
            }
        }

        public void Play()
        {
            throw new NotImplementedException();
        }

        public String Information()
        {
            return "[" + (isValid ? "Valid" : "Invalid") + "]" + Path + " " + Width + "x" + Height;
        }

        private void AddPixel(bool horizontal) {
            if (horizontal) { }
            else { }
        }


        public enum Formats {
            png, jpg, gif
        }
    }
}
