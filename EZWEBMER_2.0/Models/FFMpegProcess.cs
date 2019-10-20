using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EZWEBMER_2._0.Models
{
    static class FFMpegProcess
    {
        public static void StaticImgAndMusicVid(String p, String m, String s, int duration) {
            if (p!=null && m!=null)
            {
                FileInfo fp = new FileInfo(p);
                FileInfo fv = new FileInfo(s);
                
                String cmdtext = ((fp.Extension == ".gif") ? (" -ignore_loop 0 ") : ("-loop 1 -r 1 ")) +
                    "-i \"" + p + "\"" +
                    " -i \"" + m + "\"" +
                    " -t " + duration +
                    " -b:v 0 -crf 2 -b:a 160K -shortest -g 9999 " +
                    "-pix_fmt yuv420p -speed 0 -deadline 0 -threads 4 \"" +
                    s +"\" ";

                if (File.Exists(s)) {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Overwrite existing file?", "File " + Path.GetFileNameWithoutExtension(s) + " already exists", System.Windows.MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes) s += "-y";
                    else return;
                }

                ExecuteProcess(cmdtext);
            }
        }

        static void ExecuteProcess(String cmd) {
            String folder = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + @"\ffmpeg\";
            String cmdtext = "/k cd \"" + folder + "\" & " +
                    "ffmpeg " + cmd;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = cmdtext;
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}
