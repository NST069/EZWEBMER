using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

namespace EZWEBMER_2._0.Models
{
    class VideoInfo : MediaInfo
    {
        public String Path { get; set; }
        TimeSpan duration;
        public VideoInfo(String path) {
            Load(path);
        }

        public override void Load(string path)
        {
            this.Path = path;
            duration = GetVideoDuration(path);
        }

        public override void Play()
        {
            throw new NotImplementedException();
        }

        private static TimeSpan GetVideoDuration(string filePath)
        {
            using (var shell = ShellObject.FromParsingName(filePath))
            {
                IShellProperty prop = shell.Properties.System.Media.Duration;
                var t = (ulong)prop.ValueAsObject;
                return TimeSpan.FromTicks((long)t);
                
            }
        }

        public int getSeconds() {
            return (int)duration.TotalSeconds;
        }

        public enum Formats {
            webm, mp4
        }
    }
}
