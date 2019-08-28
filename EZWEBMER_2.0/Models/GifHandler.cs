using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace EZWEBMER_2._0.Models
{
    class GifHandler
    {
        private Image[] frames;
        public int framesCount
        {
            get
            {
                if (frames != null)
                    return frames.Length;
                return 0;
            }
        }

        public GifHandler(String path)
        {
            frames = getFrames(Image.FromFile(path));
        }
        Image[] getFrames(Image originalImg)
        {
            int numberOfFrames = originalImg.GetFrameCount(FrameDimension.Time);
            Image[] frames = new Image[numberOfFrames];

            for (int i = 0; i < numberOfFrames; i++)
            {
                originalImg.SelectActiveFrame(FrameDimension.Time, i);
                frames[i] = ((Image)originalImg.Clone());
            }
            return frames;
        }

        BitmapImage ToWpfImage(Image img)
        {
            BitmapImage ix = new BitmapImage();
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, ImageFormat.Bmp);

                    ix.BeginInit();
                    ix.CacheOption = BitmapCacheOption.OnLoad;
                    ix.StreamSource = ms;
                    ix.EndInit();
                }
            }
            catch (Exception e) { Console.WriteLine(e.StackTrace); }
            return ix;
        }

        public BitmapImage GetFrame(int frame) {
            if (frame < framesCount && frame>0) {
                return ToWpfImage(frames[frame - 1]);
            }
            return null;
        }
        public static bool isGif(String s) {
            return ((new FileInfo(s).Extension == ".gif")?true:false);
        }
    }
}
