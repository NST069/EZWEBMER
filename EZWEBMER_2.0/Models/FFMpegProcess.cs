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
        static String initialFolder
        {
            get
            {
                FileInfo fi = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\EZWEBMER\");
                if (!fi.Directory.Exists) fi.Directory.Create();
                return fi.FullName;
            }
        }
        public static void StaticImgAndMusicVid(String p, String m, String s, int duration, String format = ".webm") {

            FileInfo fp = new FileInfo(p);
            FileInfo fm = new FileInfo(m);
            String output = initialFolder + s + format;

            String cmdtext = ((fp.Extension == ".gif") ? (" -ignore_loop 0 ") : ("-loop 1 -r 1 ")) +
                "-i \"" + p + "\"" +
                " -i \"" + m + "\"" +
                " -t " + duration +
                " -b:v 0 -crf 2 -b:a 160K -shortest -g 9999 " +
                "-pix_fmt yuv420p -speed 0 -deadline 0 -threads 4 \"" +
                output + "\"";

            if (File.Exists(output))
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Overwrite existing file?", "File " + Path.GetFileNameWithoutExtension(output) + " already exists", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes) cmdtext += "-y";
                else return;
            }

            ExecuteProcess(cmdtext);

        }

        public static void AudioFromVideo(String input, String format = ".mp3") {
            FileInfo fv = new FileInfo(input);
            String output = initialFolder + Path.GetFileNameWithoutExtension(input) + format;
            String cmdtext = "-i \"" + input + "\" -ar 44100 -ac 2";
            if (format == ".mp3") cmdtext += " -vn -acodec mp3 -ab 320 ";
            else if (format == ".wav") cmdtext += " -vn -acodec pcm_s16le ";
            else if (format == ".flac") cmdtext += "-acodec flac -bits_per_raw_sample 16";
            cmdtext += " -f " + format.Substring(1) + " \"" + output + "\"";

            if (File.Exists(output))
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Overwrite existing file?", "File " + Path.GetFileNameWithoutExtension(output) + " already exists", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes) cmdtext += "-y";
                else return;
            }

            ExecuteProcess(cmdtext);
        }

        public static void VideoToGif(String input) {
            FileInfo fv = new FileInfo(input);
            String output = initialFolder + Path.GetFileNameWithoutExtension(input) + ".gif";
            String cmdtext = "-i \"" + input + "\" \"" + output + "\"";

            if (File.Exists(output))
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Overwrite existing file?", "File " + Path.GetFileNameWithoutExtension(output) + " already exists", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes) cmdtext += "-y";
                else return;
            }

            ExecuteProcess(cmdtext);
        }

        public static void GetFrame(String input, int hh, int mm, int ss, String format=".png") {
            FileInfo fv = new FileInfo(input);
            String output = initialFolder + Path.GetFileNameWithoutExtension(input) + "-snapAt-" + hh + "h" + mm + "m" + ss + "s" + format;
            String cmdtext = "-ss " + hh + ":" + mm + ":" + ss + " -i \"" + input + "\" -f image2 -vframes 1 -q:v 2 -r 1 \""
                + output + "\"";

            if (File.Exists(output))
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Overwrite existing file?", "File " + Path.GetFileNameWithoutExtension(output) + " already exists", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes) cmdtext += "-y";
                else return;
            }

            ExecuteProcess(cmdtext);
        }

        public static List<String> GetFunctions() {
            List<String> l = new List<String>();

            l.Add("Img+Music=Video");
            l.Add("Video->Music");
            l.Add("Video->Gif");
            l.Add("SnapAt");

            return l;
        }

        static void ExecuteProcess(String cmd) {
            String folder = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + @"\ffmpeg\";
            String cmdtext = "/c cd \"" + folder + "\" & " +
                    "ffmpeg " + cmd;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = cmdtext;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            if(process.ExitCode!=0)
                System.Windows.MessageBox.Show("Error "+process.ExitCode);
        }
    }
}
