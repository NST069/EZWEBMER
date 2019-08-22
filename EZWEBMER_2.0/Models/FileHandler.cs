using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZWEBMER_2._0.Models
{
    static class FileHandler
    {
        public static String OpenFile(String filter = "")
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            switch (filter)
            {
                case "Audio":
                    String sa = "";
                    foreach (MusicInfo.Formats x in Enum.GetValues(typeof(MusicInfo.Formats)))
                        sa += "*." + x + ";";
                    ofd.Filter = "Audio Files|" + sa; //TODO: Make MP3-friendly
                    break;
                case "Image":
                    String si = "";
                    foreach (ImageInfo.Formats x in Enum.GetValues(typeof(ImageInfo.Formats)))
                        si += "*." + x + ";";
                    ofd.Filter = "Images|" + si;
                    break;
                default:
                    break;
            }
            if (ofd.ShowDialog() == true) return ofd.FileName;
            return "";
        }

        public static String SaveFile(String format = "webm") {
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            String s = "";
            foreach (VideoInfo.Formats x in Enum.GetValues(typeof(VideoInfo.Formats)))
                s += "Videos|*." + x + "|";
            sfd.Filter = s.Substring(0, s.Length-1);
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            sfd.AddExtension = true;
            if (sfd.ShowDialog() == true)
            {
                if (System.IO.File.Exists(sfd.FileName)) System.IO.File.Delete(sfd.FileName);
                return sfd.FileName;
            }
            return "";
        }
    }
}
