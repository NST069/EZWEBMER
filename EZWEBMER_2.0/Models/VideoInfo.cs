using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZWEBMER_2._0.Models
{
    class VideoInfo : MediaInfo
    {
        public String Path { get; set; }
        public VideoInfo(String path) {
            Load(path);
        }

        public override void Load(string path)
        {
            this.Path = path;
        }

        public override void Play()
        {
            throw new NotImplementedException();
        }

        public enum Formats {
            webm, mp4
        }
    }
}
