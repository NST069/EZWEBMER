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
                    ofd.Filter = "Audio Files|*.wav;*.mp3";
                    break;
                case "Image":
                    ofd.Filter = "Images|*.png;*.jpg";
                    break;
                default:
                    break;
            }
            if (ofd.ShowDialog() == true) return ofd.FileName;
            return "";
        }

        public static String SaveFile(String format) {
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.Filter = "Videos|*."+format;
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            sfd.AddExtension = true;
            if (sfd.ShowDialog() == true) return sfd.FileName;
            return "";
        }
    }
}
