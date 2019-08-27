using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace EZWEBMER_2._0.Models
{
    class ImageInfo
    {
        public bool isValid;
        public String Path { get; set; }
        private BitmapImage image;
        public int Height;
        public int Width;

        public ImageInfo(String path) {
            isValid = false;
            this.Path = path;
            image = new BitmapImage(new Uri(path));
            Height = image.PixelHeight;
            Width = image.PixelWidth;

            if (Height % 2 != 0)
            {
                System.Windows.MessageBox.Show("Wrong Height. Must be even");
                //AddPixel(true);
                return;
            }
            if (Width % 2 != 0) {
                System.Windows.MessageBox.Show("Wrong Width. Must be even");
                //AddPixel(false);
                return;
            }
            isValid = true;
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
