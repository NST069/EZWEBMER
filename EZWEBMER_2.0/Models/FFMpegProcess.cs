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
    class RenderCommand
    {
        public String name;
        Func<String, String, String, String, String> cmdGetter; //picture, audio, video, format, OUT:command
        static String initialFolder
        {
            get
            {
                FileInfo fi = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\EZWEBMER\");
                if (!fi.Directory.Exists) fi.Directory.Create();
                return fi.FullName;
            }
        }

        public List<String> availableFormats;

        public RenderCommand(String cmdName)
        {
            name = cmdName;
            availableFormats = new List<String>();
            switch (cmdName)
            {
                case "Img+Music=Video":
                    cmdGetter = ImgAndMusic2Vid;
                    foreach (Models.VideoInfo.Formats x in Enum.GetValues(typeof(Models.VideoInfo.Formats)))
                        availableFormats.Add("." + x);
                    break;
                case "Video->Music":
                    cmdGetter = Vid2Aud;
                    foreach (Models.MusicInfo.Formats x in Enum.GetValues(typeof(Models.MusicInfo.Formats)))
                        availableFormats.Add("." + x);
                    break;
                case "Video->Gif":
                    cmdGetter = Vid2Gif;
                    availableFormats.Add(".gif");
                    break;
                case "Gif->Video":
                    cmdGetter = Gif2Vid;
                    foreach (Models.VideoInfo.Formats x in Enum.GetValues(typeof(Models.VideoInfo.Formats)))
                        availableFormats.Add("." + x);
                    break;
                default:
                    cmdGetter = null;
                    break;

            }
        }
        public String GetCommand(ImageInfo pic = null, MusicInfo aud = null, VideoInfo vid = null, String fmt = "")
        {
            return cmdGetter(pic?.Path, aud?.Path, vid?.Path, fmt);
        }


        String ImgAndMusic2Vid(String p, String m, String v, String format = ".webm")
        {
            FileInfo fp = new FileInfo(p);
            FileInfo fm = new FileInfo(m);
            int duration = (new MusicInfo(m)).getSeconds();
            String output = initialFolder + v + format;

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
                else return "";
            }

            return cmdtext;
        }

        String Vid2Aud(String p, String m, String v, String format = ".webm")
        {
            FileInfo fv = new FileInfo(v);
            String output = initialFolder + Path.GetFileNameWithoutExtension(v) + format;
            String cmdtext = "-i \"" + v + "\" -ar 44100 -ac 2";
            if (format == ".mp3") cmdtext += " -vn -acodec mp3 -ab 320 ";
            else if (format == ".wav") cmdtext += " -vn -acodec pcm_s16le ";
            else if (format == ".flac") cmdtext += "-acodec flac -bits_per_raw_sample 16";
            cmdtext += " -f " + format.Substring(1) + " \"" + output + "\"";

            if (File.Exists(output))
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Overwrite existing file?", "File " + Path.GetFileNameWithoutExtension(output) + " already exists", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes) cmdtext += "-y";
                else return "";
            }

            return cmdtext;
        }
        String Vid2Gif(String p, String m, String v, String format = ".webm")
        {
            FileInfo fv = new FileInfo(v);
            String output = initialFolder + Path.GetFileNameWithoutExtension(v) + ".gif";
            String cmdtext = "-i \"" + v + "\" \"" + output + "\"";

            if (File.Exists(output))
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Overwrite existing file?", "File " + Path.GetFileNameWithoutExtension(output) + " already exists", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes) cmdtext += "-y";
                else return "";
            }

            return cmdtext;
        }
        String Gif2Vid(String p, String m, String v, String format = ".webm")
        {
            FileInfo fp = new FileInfo(p);
            String output = initialFolder + v + format;

            String cmdtext = "-i \"" + p +
                "-movflags faststart -pix_fmt yuv420p -vf \"scale = trunc(iw / 2) * 2:trunc(ih / 2) * 2\" " +
                output;

            if (File.Exists(output))
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Overwrite existing file?", "File " + Path.GetFileNameWithoutExtension(output) + " already exists", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes) cmdtext += "-y";
                else return "";
            }

            return cmdtext;
        }

        public override string ToString()
        {
            return name;
        }
    }

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
        static List<RenderCommand> cmdList = new List<RenderCommand>();

        static FFMpegProcess() {
            cmdList.Add(new RenderCommand("Img+Music=Video"));
            cmdList.Add(new RenderCommand("Video->Music"));
            cmdList.Add(new RenderCommand("Video->Gif"));
            cmdList.Add(new RenderCommand("Gif->Video"));
            //cmdList.Add("SnapAt");
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

        public static List<RenderCommand> GetFunctions() {
            List<RenderCommand> l = new List<RenderCommand>(cmdList);
            return l;
        }

        public static void ExecuteProcess(String cmd) {
            String folder = Unosquare.FFME.Library.FFmpegDirectory;
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
